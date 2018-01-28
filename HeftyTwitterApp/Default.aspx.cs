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
            Twitter twitter = new Twitter();
            twitter.GetTweets(srch1, srch2);

            //store in DB
            DBInteractions DBI = new DBInteractions();
            DBI.StoreInDB(search1.Text, search2.Text);
        }
    }
}