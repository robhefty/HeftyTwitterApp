using System;
using System.Web;
using System.Web.UI;

namespace HeftyTwitterApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submitsearch_Click(object sender, EventArgs e)
        {
            //remove unwanted chars
            Utility util = new Utility();
            string srch1 = util.CleanInput(search1.Text);
            string srch2 = util.CleanInput(search2.Text);

            //search twitter
            MyTwitterObj MyTwitter = new MyTwitterObj();
            MyTwitter.search_1 = srch1;
            MyTwitter.search_2 = srch2;
            Twitter twitter = new Twitter();
            MyTwitter = twitter.GetData(MyTwitter);

            //store in DB
            DBInteractions DBI = new DBInteractions();
            DBI.StoreInDB(search1.Text, MyTwitter.tweets_1, search2.Text, MyTwitter.tweets_2, MyTwitter.seconds_1, MyTwitter.seconds_2);

            //go to next page
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("srch1", search1.Text);
            CurrContext.Items.Add("srch2", search2.Text);
            CurrContext.Items.Add("tweets1", MyTwitter.tweets_1);
            CurrContext.Items.Add("tweets2", MyTwitter.tweets_2);
            CurrContext.Items.Add("seconds1", MyTwitter.seconds_1);
            CurrContext.Items.Add("seconds2", MyTwitter.seconds_2);
            Server.Transfer("Results.aspx");
        }
    }
}