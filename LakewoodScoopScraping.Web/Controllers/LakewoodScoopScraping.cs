using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LakewoodScoopScraping.Web.Services;

namespace LakewoodScoopScraping.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LakewoodScoopScraping : ControllerBase
    {
        [Route("Scrape")]
        public List<NewsTidbit> Scrape()
        {
            var scraper = new Scraper();
            return scraper.Scrape();
        }
    }
}
