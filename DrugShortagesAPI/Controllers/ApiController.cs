using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DrugShortagesAPI;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DrugShortagesAPI.Controllers
{
    [Route("{feed}")]
    [Route("api/{feed}")]
    public class ApiController : Controller
    {
        //These should all be moved to appsettings.json or ENV eventually
        private string shortageURL = "https://www.ashp.org/rss/shortages/";
        private string discontinuedURL = "https://www.ashp.org/rss/notavailable/";
        private string resolvedURL = "https://www.ashp.org/rss/resolved/";
        private string notavailableURL = "https://www.ashp.org/rss/nopresentations";

        public async Task<List<RssParser.FeedItem>> GetItemsAsync(string feed)
        {
            RssParser rssParser = new RssParser();
            switch (feed.ToLower())
            {
                case "shortage":
                case "shortages":
                    return await rssParser.GetListAsync(shortageURL);
                case "dc":
                case "discontinued":
                    return await rssParser.GetListAsync(discontinuedURL);
                case "resolved":
                    return await rssParser.GetListAsync(resolvedURL);
                case "unavailable":
                case "notavailable":
                    return await rssParser.GetListAsync(notavailableURL);
                default:
                    return await rssParser.GetListAsync(shortageURL);
            }
        }

    }
}
