using DTuriasConnectorTwitter.Config;
using DTuriasCore.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Tweetinvi;

namespace DTuriasConnectorTwitter.Http
{
    class Manager
    {
        static ILog _logger = LogManager.GetLogger(typeof(Manager));
        HttpClient client;

        public Manager(StreamReader fileProperties)
        {
            var properties = JsonConvert.DeserializeObject<Properties>(fileProperties.ReadToEnd());
            //Set up your credentials (https://apps.twitter.com)
            var consumerKey = properties.user.twitter.consumerKey;
            var consumerSecret = properties.user.twitter.consumerSecret;
            var accessToken = properties.user.twitter.token;
            var accessTokenSecret = properties.user.twitter.tokenSecret;

            DTuriasUserNick = properties.user.nick;
            DatabaseUrl = properties.database.url;

            Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);

            client = new HttpClient()
            {
                BaseAddress = new Uri(properties.database.url)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public String DTuriasUserNick { get; set; }

        public String DatabaseUrl { get; set; }

        public HttpResponseMessage CreateTweet(TweetModel tweetModel)
        {
            return Create("/api/tweets", tweetModel);
        }

        public HttpResponseMessage Create(String path, Object toCreate)
        {
            _logger.Info(JsonConvert.SerializeObject(toCreate));
            //_logger.Info(path);
            var content = new StringContent(JsonConvert.SerializeObject(toCreate), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(path, content).Result;
            _logger.Info(response.StatusCode);
            //_logger.Info(response.Headers.Location);
            return response;
        }
    }
}
