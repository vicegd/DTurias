using DTuriasConnectorTwitter.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;

namespace DTuriasConnectorTwitter
{
    class Manager
    {
        public Manager(StreamReader fileProperties)
        {
            var properties = JsonConvert.DeserializeObject<Properties>(fileProperties.ReadToEnd());
            //Set up your credentials (https://apps.twitter.com)
            var consumerKey = properties.user.twitter.consumerKey;
            var consumerSecret = properties.user.twitter.consumerSecret;
            var accessToken = properties.user.twitter.token;
            var accessTokenSecret = properties.user.twitter.tokenSecret;

            UserId = properties.user.id;
            UserName = properties.user.name;
            DatabaseUrl = properties.database.url;

            Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);
        }

        public String UserId { get; set; }

        public String UserName { get; set; }

        public String DatabaseUrl { get; set; }
    }
}
