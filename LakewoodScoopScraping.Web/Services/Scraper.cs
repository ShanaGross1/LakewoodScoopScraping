using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System.Xml.Linq;

namespace LakewoodScoopScraping.Web.Services
{
    public class NewsTidbit
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Text { get; set; }
        public int CommentsCount { get; set; }
        public string Url { get; set; }
        public string PostedDate { get; set; }
    }

    public class Scraper
    {
        public List<NewsTidbit> Scrape()
        {
            var html = GetLakewoodScoopHtml();
            var document = new HtmlParser().ParseDocument(html);
            var resultDivs = document.QuerySelectorAll("div.td-category-pos-image");
            return resultDivs.Select(div => ParseItem(div)).Where(i => i != null).ToList();
        }

        private string GetLakewoodScoopHtml()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate,
                UseCookies = true
            };
            var client = new HttpClient(handler);

            return client.GetStringAsync("https://thelakewoodscoop.com/").Result;
        }

        private NewsTidbit ParseItem(IElement div)
        {
            var titleElement = div.QuerySelector("h3.td-module-title");
            var textElement = div.QuerySelector("div.td-excerpt");
            var commentsElement = div.QuerySelector("span.td-module-comments");
            var imageElement = div.QuerySelector("span.entry-thumb");
            var postedDate = div.QuerySelector("span.td-post-date");
            var anchorTag = div.QuerySelector("h3.td-module-title a");


            return new NewsTidbit
            {
                Title = titleElement != null ? titleElement.TextContent : "",
                Text = textElement != null ? textElement.TextContent : "",
                Image = imageElement != null ? imageElement.Attributes["data-img-url"].Value : "",
                CommentsCount = commentsElement != null ? int.Parse(commentsElement.TextContent) : 0,
                PostedDate = postedDate != null ? postedDate.TextContent : "",
                Url = anchorTag != null ? anchorTag.Attributes["href"].Value : ""
            };

        }
    }
}
