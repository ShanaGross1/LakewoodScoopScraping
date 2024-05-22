import axios from 'axios'
import React, { useState, useEffect } from 'react';

const Home = () => {
    const [newsTidbitz, setNewsTidbitz] = useState();

    useEffect(() => {
        const loadTidbitz = async () => {
            const { data } = await axios.get('/api/lakewoodscoopscraping/scrape')
            setNewsTidbitz(data);
        }
        loadTidbitz();
    }, [])


    return (
        <>
            <h1 className="text-center" style={{ marginTop: 80 }}>
                <img className="tdb-logo-img" src="https://lakewoodscoop.b-cdn.net/wp-content/uploads/2022/05/thelakewoodscoop_logo.png" alt="Logo" title=""></img>
                THE LAKEWOOD SCOOP - Ads free!!
            </h1>
            {newsTidbitz && newsTidbitz.map((tidbit, index) => (
                <div key={index} className="td-block-title-wrap mt-5">
                    <div className="td_module_flex td_module_flex_5 td_module_wrap td-animation-stack ">
                        <div className="container">
                            <div className="td-module-container td-category-pos-image">
                                <div className="td-module-meta-info td-module-meta-info-top">
                                    <h3 className="entry-title td-module-title">
                                        <a href={tidbit.url} rel="bookmark" title={tidbit.title}>
                                            {tidbit.title}</a>
                                    </h3>
                                </div>
                                <div className="td-image-container">
                                    <div className="td-module-thumb">
                                        <img className="td-image-wrap" style={{ height: 250 }}
                                            src={tidbit.image} >
                                        </img>
                                    </div>
                                </div>
                                <div className="td-module-meta-info td-module-meta-info-bottom">
                                    <div className="td-editor-date">
                                        <span className="td-author-date">
                                            <span className="td-post-date">
                                                <time className="entry-date updated td-module-date" >{tidbit.postedDate} Comments: {tidbit.commentsCount}</time>
                                            </span>
                                        </span>
                                    </div>
                                    <div className="td-excerpt"> {tidbit.text}

                                    </div>
                                    <div className="td-read-more">
                                        <a href={tidbit.url}>Read more</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            ))}
        </>)
}

export default Home;

