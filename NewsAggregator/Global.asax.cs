using System;
using System.Collections.Generic;
using System.Linq;
using ReadSharp;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.ServiceModel.Syndication;
using System.Web.Security;
using System.Web.SessionState;

namespace NewsAggregator
{
    public class Global : HttpApplication
    {
        static public List<Article> listOfItem = new List<Article>();

        async void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            try
            {
                List<Article> dummy = await RSSParser.parseRSS("http://rss.detik.com/index.php/detikcom");
                foreach (Article item in dummy)
                {
                    Global.listOfItem.Add(item);
                }
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}