using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace HeftyTwitterApp
{
    public class Twitter
    {
        public MyTwitterObj GetData(MyTwitterObj myTwitter)
        {
            //first search:
            TwitterHelper helper1 = new TwitterHelper
            {
                search = myTwitter.search_1,
                max_id = 0,
                since_id = 0,
                count = 0
            };
            
            RootObject root1 = GetTweets(helper1);
            if (root1.statuses.Count > 0)
            {
                //if (root1.statuses.Count == Int32.Parse(ConfigurationManager.AppSettings["TwitterMaxResults"]))
                //{

                //}
                foreach (Status value in root1.statuses)
                {
                    helper1.count = helper1.count + value.favorite_count + value.retweet_count + 1;
                }
            }

            //second search: 
            TwitterHelper helper2 = new TwitterHelper
            {
                search = myTwitter.search_2,
                max_id = 0,
                since_id = 0,
                count = 0
            };

            RootObject root2 = GetTweets(helper2);
            if (root2.statuses.Count > 0)
            {
                foreach (Status value in root2.statuses)
                {
                    helper2.count = helper2.count + value.favorite_count + value.retweet_count + 1;
                }
            }

            //return data
            MyTwitterObj RetTwitterObj = new MyTwitterObj();
            RetTwitterObj.search_1 = myTwitter.search_1;
            RetTwitterObj.search_2 = myTwitter.search_2;
            RetTwitterObj.tweets_1 = helper1.count;
            RetTwitterObj.tweets_2 = helper2.count;
            return RetTwitterObj;
        }

        public RootObject GetTweets(TwitterHelper helper)
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
            var searchURL = string.Format(searchFormat, helper.search, helper.since_id, helper.max_id, maxResults);
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
                    //RootObject root = new RootObject();
                    return JsonConvert.DeserializeObject<RootObject>(searchJson);
                }
            }
        }
    }

    public class TwitterHelper
    {
        public string search { get; set; }
        public int max_id { get; set; }
        public int since_id { get; set; }
        public int count { get; set; }
    }

    public class MyTwitterObj
    {
        public string search_1 { get; set; }
        public int tweets_1 { get; set; }
        public string search_2 { get; set; }
        public int tweets_2 { get; set; }
        public int seconds { get; set; }
    }

    public class TwitAuthenticateResponse
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
    }

    public class UserMention
    {
        //public string screen_name { get; set; }
        //public string name { get; set; }
        public int id { get; set; }
        public string id_str { get; set; }
        public List<int> indices { get; set; }
    }

    //public class Entities
    //{
    //    public List<object> hashtags { get; set; }
    //    public List<object> symbols { get; set; }
    //    public List<UserMention> user_mentions { get; set; }
    //    public List<object> urls { get; set; }
    //}

    //public class Metadata
    //{
    //    public string iso_language_code { get; set; }
    //    public string result_type { get; set; }
    //}

    //public class Description
    //{
    //    public List<object> urls { get; set; }
    //}

    //public class Entities2
    //{
    //    public Description description { get; set; }
    //}

    //public class User
    //{
    //    public long id { get; set; }
    //    public string id_str { get; set; }
    //    public string name { get; set; }
    //    public string screen_name { get; set; }
    //    public string location { get; set; }
    //    public string description { get; set; }
    //    public object url { get; set; }
    //    public Entities2 entities { get; set; }
    //    public bool @protected { get; set; }
    //    public int followers_count { get; set; }
    //    public int friends_count { get; set; }
    //    public int listed_count { get; set; }
    //    public string created_at { get; set; }
    //    public int favourites_count { get; set; }
    //    public object utc_offset { get; set; }
    //    public object time_zone { get; set; }
    //    public bool geo_enabled { get; set; }
    //    public bool verified { get; set; }
    //    public int statuses_count { get; set; }
    //    public string lang { get; set; }
    //    public bool contributors_enabled { get; set; }
    //    public bool is_translator { get; set; }
    //    public bool is_translation_enabled { get; set; }
    //    public string profile_background_color { get; set; }
    //    public string profile_background_image_url { get; set; }
    //    public string profile_background_image_url_https { get; set; }
    //    public bool profile_background_tile { get; set; }
    //    public string profile_image_url { get; set; }
    //    public string profile_image_url_https { get; set; }
    //    public string profile_banner_url { get; set; }
    //    public string profile_link_color { get; set; }
    //    public string profile_sidebar_border_color { get; set; }
    //    public string profile_sidebar_fill_color { get; set; }
    //    public string profile_text_color { get; set; }
    //    public bool profile_use_background_image { get; set; }
    //    public bool has_extended_profile { get; set; }
    //    public bool default_profile { get; set; }
    //    public bool default_profile_image { get; set; }
    //    public object following { get; set; }
    //    public object follow_request_sent { get; set; }
    //    public object notifications { get; set; }
    //    public string translator_type { get; set; }
    //}

    //public class Entities3
    //{
    //    public List<object> hashtags { get; set; }
    //    public List<object> symbols { get; set; }
    //    public List<object> user_mentions { get; set; }
    //    public List<object> urls { get; set; }
    //}

    //public class Metadata2
    //{
    //    public string iso_language_code { get; set; }
    //    public string result_type { get; set; }
    //}

    //public class Description2
    //{
    //    public List<object> urls { get; set; }
    //}

    //public class Entities4
    //{
    //    public Description2 description { get; set; }
    //}

    //public class User2
    //{
    //    public int id { get; set; }
    //    public string id_str { get; set; }
    //    public string name { get; set; }
    //    public string screen_name { get; set; }
    //    public string location { get; set; }
    //    public string description { get; set; }
    //    public object url { get; set; }
    //    public Entities4 entities { get; set; }
    //    public bool @protected { get; set; }
    //    public int followers_count { get; set; }
    //    public int friends_count { get; set; }
    //    public int listed_count { get; set; }
    //    public string created_at { get; set; }
    //    public int favourites_count { get; set; }
    //    public int utc_offset { get; set; }
    //    public string time_zone { get; set; }
    //    public bool geo_enabled { get; set; }
    //    public bool verified { get; set; }
    //    public int statuses_count { get; set; }
    //    public string lang { get; set; }
    //    public bool contributors_enabled { get; set; }
    //    public bool is_translator { get; set; }
    //    public bool is_translation_enabled { get; set; }
    //    public string profile_background_color { get; set; }
    //    public string profile_background_image_url { get; set; }
    //    public string profile_background_image_url_https { get; set; }
    //    public bool profile_background_tile { get; set; }
    //    public string profile_image_url { get; set; }
    //    public string profile_image_url_https { get; set; }
    //    public string profile_banner_url { get; set; }
    //    public string profile_link_color { get; set; }
    //    public string profile_sidebar_border_color { get; set; }
    //    public string profile_sidebar_fill_color { get; set; }
    //    public string profile_text_color { get; set; }
    //    public bool profile_use_background_image { get; set; }
    //    public bool has_extended_profile { get; set; }
    //    public bool default_profile { get; set; }
    //    public bool default_profile_image { get; set; }
    //    public object following { get; set; }
    //    public object follow_request_sent { get; set; }
    //    public object notifications { get; set; }
    //    public string translator_type { get; set; }
    //}

    public class RetweetedStatus
    {
        //public string created_at { get; set; }
        //public long id { get; set; }
        //public string id_str { get; set; }
        //public string text { get; set; }
        //public bool truncated { get; set; }
        //public Entities3 entities { get; set; }
        //public Metadata2 metadata { get; set; }
        //public string source { get; set; }
        //public object in_reply_to_status_id { get; set; }
        //public object in_reply_to_status_id_str { get; set; }
        //public object in_reply_to_user_id { get; set; }
        //public object in_reply_to_user_id_str { get; set; }
        //public object in_reply_to_screen_name { get; set; }
        //public User2 user { get; set; }
        //public object geo { get; set; }
        //public object coordinates { get; set; }
        //public object place { get; set; }
        //public object contributors { get; set; }
        //public bool is_quote_status { get; set; }
        public int retweet_count { get; set; }
        public int favorite_count { get; set; }
        //public bool favorited { get; set; }
        public bool retweeted { get; set; }
        //public string lang { get; set; }
    }

    public class Status
    {
        public string created_at { get; set; }
        public long id { get; set; }
        public string id_str { get; set; }
        //public string text { get; set; }
        //public bool truncated { get; set; }
        //public Entities entities { get; set; }
        //public Metadata metadata { get; set; }
        //public string source { get; set; }
        //public object in_reply_to_status_id { get; set; }
        //public object in_reply_to_status_id_str { get; set; }
        //public object in_reply_to_user_id { get; set; }
        //public object in_reply_to_user_id_str { get; set; }
        //public object in_reply_to_screen_name { get; set; }
        //public User user { get; set; }
        //public object geo { get; set; }
        //public object coordinates { get; set; }
        //public object place { get; set; }
        //public object contributors { get; set; }
        public RetweetedStatus retweeted_status { get; set; }
        //public bool is_quote_status { get; set; }
        public int retweet_count { get; set; }
        public int favorite_count { get; set; }
        public bool favorited { get; set; }
        public bool retweeted { get; set; }
        //public string lang { get; set; }
    }

    public class SearchMetadata
    {
        public double completed_in { get; set; }
        public int max_id { get; set; }
        public string max_id_str { get; set; }
        public string next_results { get; set; }
        public string query { get; set; }
        public int count { get; set; }
        public int since_id { get; set; }
        public string since_id_str { get; set; }
    }

    public class RootObject
    {
        public List<Status> statuses { get; set; }
        public SearchMetadata search_metadata { get; set; }
    }
}