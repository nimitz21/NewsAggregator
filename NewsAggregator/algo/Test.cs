using ReadSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;

namespace NewsAggregator
{
    public class Test
    {
        public Test()
        {

        }
        static public String testing()
        {
            String test = "<html>";
            try
            {
                foreach (KeyValuePair<String, Article> pair in Global.listOfItem)
                {
                    Article item = pair.Value;
                    test += "JINGJING" + "<br>";
                    test += item.Title+ "<br>";
                    foreach (ArticleImage image in item.Images)
                    {
                        test += "<img src=\"" + image.Uri + "\">" + "<br>";
                        break;
                    }
                    test += "<hr";
                    //test += item.Content + "<hr>";
                }
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
            test += "</html>";
            return test;
        }
    }
}