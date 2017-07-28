using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;

namespace DTuriasConnectorTwitter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up your credentials (https://apps.twitter.com)
            string consumerKey = "qvCHrYNQ8O8ptdqmc87Yoc9wo";
            string consumerSecret = "oFIYsNH2uBmmaAV4saXWia8eUvmBcV16CLV6u19zNt5IYIrIeA";
            string accessToken = "870224367510278144-eFdOf9JfIgfQu14v6j5B0pgEFRiuykP";
            string accessTokenSecret = "EohoGqPtxlXY4WTc6S6eDs8Hofz6uWEbvJZG8B0l4vQ2c";
            Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);

            // Publish the Tweet "Hello World" on your Timeline
            Tweet.PublishTweet("Hello World!");
        }
    }
}
