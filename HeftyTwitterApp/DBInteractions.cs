using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HeftyTwitterApp
{
    class DBInteractions
    {
        Utility util = new Utility();
        public void StoreInDB(string search1, string search2)
        {
            if (search1.Length > 0 && search2.Length > 0)
            {
                string connstr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connstr))
                {
                    using (SqlCommand cmd = new SqlCommand("InsertSearches", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        con.Open();
                        cmd.Parameters.AddWithValue("@search1", search1);
                        cmd.Parameters.AddWithValue("@tweets1", "23");
                        cmd.Parameters.AddWithValue("@search2", search2);
                        cmd.Parameters.AddWithValue("@tweets2", "234");
                        cmd.Parameters.AddWithValue("@seconds", "34");
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }

        public DataTable GetData()
        {
            string connstr = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connstr))
            {
                using (SqlCommand cmd = new SqlCommand("GetSearches", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
    }
}