using System;
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
            DBI.StoreInDB(search1.Text, MyTwitter.tweets_1, search2.Text, MyTwitter.tweets_2);
        }
    }
}