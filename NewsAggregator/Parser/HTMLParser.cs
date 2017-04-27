using ReadSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NewsAggregator
{
    public class HTMLParser
    {
        static async public Task<Article> parseHTML(string url)
        {
            Reader reader = new Reader();
            Article article = null;
            try
            {
                article = await reader.Read(new Uri(url));
            }
            catch (ReadException exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.StackTrace);
            }
            return article;
        }
    }
}