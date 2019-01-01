using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DrugShortagesAPI;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DrugShortagesAPI.Controllers
{
    [Route("api/shortages")]
    public class ShortageController : Controller
    {
        private string ShortageURL = "https://www.ashp.org/rss/shortages/";
        // GET: api/values
        [HttpGet]
        public async Task<List<RssParser.FeedItem>> GetAsync()
        {
            RssParser shortages = new RssParser(); 
            return await shortages.GetListAsync(ShortageURL);
        }

    }

    [Route("api/discontinued")]
    public class UnavilableController : Controller
    {
        private string UnavailableURL = "https://www.ashp.org/rss/notavailable/";
        [HttpGet]
        public async Task<List<RssParser.FeedItem>> GetAsync()
        {
            RssParser discontinued = new RssParser();
            return await discontinued.GetListAsync(UnavailableURL);
        }
    }

    [Route("api/resolved")]
    public class ResolvedController : Controller
    {
        private string resolvedURL = "https://www.ashp.org/rss/resolved/";
        [HttpGet]
        public async Task<List<RssParser.FeedItem>> GetAsync()
        {
            RssParser resolved = new RssParser();
            return await resolved.GetListAsync(resolvedURL);
        }
    }


    [Route("api/notavailable")]
    public class NotAvailableController : Controller
    {
        private string notavailableURL = "https://www.ashp.org/rss/nopresentations";
        [HttpGet]
        public async Task<List<RssParser.FeedItem>> GetAsync()
        {
            RssParser notavailable = new RssParser();
            return await notavailable.GetListAsync(notavailableURL);
        }
    }
}
