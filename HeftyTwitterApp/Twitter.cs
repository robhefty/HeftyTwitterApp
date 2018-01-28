using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace HeftyTwitterApp
{
    public class Twitter
    {
        public void GetTweets(string search1, string search2, string max_id="0", string since_id ="0")
        {
            // set the keys
            string oAuthConsumerKey = ConfigurationManager.AppSettings["TwitterKey"];
            var oAuthConsumerSecret = ConfigurationManager.AppSettings["TwitterSecret"];
            var oAuthUrl = ConfigurationManager.AppSettings["TwitterAuthURL"];

            // authenticate
            var authHeaderFormat = "Basic {0}";

            var authHeader = string.Format(authHeaderFormat,
                Convert.ToBase64String(Encoding.UTF8.GetBytes(Uri.EscapeDataString(oAuthConsumerKey) + ":" +
                Uri.EscapeDataString((oAuthConsumerSecret)))
            ));

            var postBody = "grant_type=client_credentials";

            //create request
            HttpWebRequest authRequest = (HttpWebRequest)WebRequest.Create(oAuthUrl);
            authRequest.Headers.Add("Authorization", authHeader);
            authRequest.Method = "POST";
            authRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            authRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (Stream stream = authRequest.GetRequestStream())
            {
                byte[] content = ASCIIEncoding.ASCII.GetBytes(postBody);
                stream.Write(content, 0, content.Length);
            }

            authRequest.Headers.Add("Accept-Encoding", "gzip");

            // deserialize
            WebResponse authResponse = authRequest.GetResponse();
            TwitAuthenticateResponse twitAuthResponse;
            using (authResponse)
            {
                using (var reader = new StreamReader(authResponse.GetResponseStream()))
                {
                    var objectText = reader.ReadToEnd();
                    twitAuthResponse = JsonConvert.DeserializeObject<TwitAuthenticateResponse>(objectText);
                }
            }

            // Do the search
            string maxResults = ConfigurationManager.AppSettings["TwitterMaxResults"];
            var searchFormat = ConfigurationManager.AppSettings["TwitterSearchURL"];
            var searchURL = string.Format(searchFormat, search1, maxResults);
            HttpWebRequest searchRequest = (HttpWebRequest)WebRequest.Create(searchURL);
            var timelineHeaderFormat = "{0} {1}";
            searchRequest.Headers.Add("Authorization", string.Format(timelineHeaderFormat, twitAuthResponse.token_type, twitAuthResponse.access_token));
            searchRequest.Method = "Get";
            WebResponse searchResponse = searchRequest.GetResponse();
            var searchJson = string.Empty;
            using (searchResponse)
            {
                using (var reader = new StreamReader(searchResponse.GetResponseStream()))
                {
                    searchJson = reader.ReadToEnd();
                }
            }
        }
    }

    public class TwitAuthenticateResponse
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
    }
}