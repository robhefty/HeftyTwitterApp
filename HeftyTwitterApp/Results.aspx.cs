using System;
using System.Web;

namespace HeftyTwitterApp
{
    public partial class Results : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext CurrContext = HttpContext.Current;
            string srch1 = CurrContext.Items["srch1"].ToString();
            string srch2 = CurrContext.Items["srch2"].ToString();
            string tweets1 = CurrContext.Items["tweets1"].ToString();
            int t1 = Int32.Parse(tweets1);
            string tweets2 = CurrContext.Items["tweets2"].ToString();
            int t2 = Int32.Parse(tweets2);
            string seconds1 = CurrContext.Items["seconds1"].ToString();
            int s1 = Int32.Parse(seconds1);
            string seconds2 = CurrContext.Items["seconds2"].ToString();
            int s2 = Int32.Parse(seconds2);

            search1.Text = srch1;
            search2.Text = srch2;

            double dbl = 0;
            if (s1 == 0)
            {
                tmp1.Text = "0.0000";
            }
            else
            {
                dbl = (double)t1 / s1;
                tmp1.Text = String.Format("{0:0.####}", dbl);
            }

            if (s2 == 0)
            {
                tmp2.Text = "0.0000";
            }
            else
            {
                dbl = (double)t2 / s2;
                tmp2.Text = String.Format("{0:0.####}", dbl);
            }
        }
    }
}