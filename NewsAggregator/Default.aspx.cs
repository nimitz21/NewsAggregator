using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewsAggregator
{
    public partial class _Default : Page
    {
        async protected void Page_Load(object sender, EventArgs e)
        {
            int x = await RSSParser.parseRSS("http://rss.detik.com/index.php/detikcom");
        }
    }
}