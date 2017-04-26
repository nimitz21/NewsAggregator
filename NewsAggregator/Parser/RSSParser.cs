﻿using ReadSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace NewsAggregator
{
    public class RSSParser
    {
        static async public Task<int> parseRSS(string url)
        {
            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();
            foreach (SyndicationItem item in feed.Items)
            {
                String title = item.Title.Text;
                String link = item.Id;
                DateTime publish_date = item.PublishDate.DateTime;
                Article article = await HTMLParser.parseHTML(link);
                System.Diagnostics.Debug.WriteLine(article.Title);
                System.Diagnostics.Debug.WriteLine(article.Content);
                foreach(ArticleImage image in article.Images)
                {
                    System.Diagnostics.Debug.WriteLine(image.Uri);
                }
            }
            return 1;
        }
    }
}