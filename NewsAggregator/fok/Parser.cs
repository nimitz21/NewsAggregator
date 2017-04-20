using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Xml;

namespace NewsAggregator
{
    public class Parser
    {
        public Parser(string url)
        { 
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            foreach (SyndicationItem item in feed.Items)
            {
                String title = item.Title.Text;
                String link = item.Id;
                DateTime publish_date = item.PublishDate.Date;
                System.Diagnostics.Debug.WriteLine(title + " " + link);
            }
        }
    }
}