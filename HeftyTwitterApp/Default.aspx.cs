using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace HeftyTwitterApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            search1.Text = "no";
        }

        protected void submitsearch_Click(object sender, EventArgs e)
        {
            //Twitter twitter = new Twitter();
            //twitter.GetTweets();

            string srch1 = search1.Text; // Scrub user data
            string srch2 = search2.Text; // Scrub user data

            string connstr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connstr))
            {
                //string query = "INSERT INTO Customers(Name, Country) VALUES(@Name, @Country)";
                using (SqlCommand cmd = new SqlCommand("InsertSearches", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@search1", srch1);
                    cmd.Parameters.AddWithValue("@tweets1", "23");
                    cmd.Parameters.AddWithValue("@search2", srch2);
                    cmd.Parameters.AddWithValue("@tweets2", "234");
                    cmd.Parameters.AddWithValue("@seconds", "34");
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}