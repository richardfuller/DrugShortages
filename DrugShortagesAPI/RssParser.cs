using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DrugShortagesAPI
{
    public class RssParser
    {

        public async Task<List<FeedItem>> GetListAsync(string feedUrl)
        {
            var articles = new List<FeedItem>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(feedUrl);
                var responseMessage = await client.GetAsync(feedUrl);
                var responseString = await responseMessage.Content.ReadAsStringAsync();

                //extract feed items
                XDocument doc = XDocument.Parse(responseString);
                var feedItems = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                                select new FeedItem
                                {
                                    uid = item.Elements().First(i => i.Name.LocalName == "guid").Value,
                                    //I'm being a little snarky here and I have no users of this skill..
                                    mainText = "Oh god another one: " + item.Elements().First(i => i.Name.LocalName == "description").Value,
                                    redirectionUrl = item.Elements().First(i => i.Name.LocalName == "link").Value,
                                    //Will receive a JSON formatting error if this parameter is not parsed to UTC.
                                    updateDate = DateTime.Parse(item.Elements().First(i => i.Name.LocalName == "pubDate").Value).ToUniversalTime(),
                                    titleText = item.Elements().First(i => i.Name.LocalName == "title").Value
                                };
                //Limit feed items to previous 7 days per Flash Briefing dev specifications
                articles = feedItems.ToList().FindAll(i => i.updateDate >= DateTime.Today.AddDays(-7));
                return articles;
            }
        }
        //When this object is serialized to JSON, it will match the required parameters for Amazon Alexa's Flash Briefing
        public class FeedItem
        {
            public string uid { get; set; }
            public DateTime updateDate { get; set; }
            public string titleText { get; set; }
            public string mainText { get; set; }
            public string redirectionUrl { get; set; }
        }
    }
}
