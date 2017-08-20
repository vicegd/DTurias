using DTuriasConnectorTwitter.Config;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace DTuriasConnectorTwitter.Tasks
{
    class MessageTask
    {
        static ILog _logger = LogManager.GetLogger(typeof(MessageTask));
        MessagesToSearch message;
        Manager manager;

        public MessageTask(Manager manager, MessagesToSearch message)
        {
            this.manager = manager;
            this.message = message;
        }

        public void Run()
        {
            foreach (var value in message.queries)
            {
                _logger.Info("*********************************************************");
                var searchParameter = new SearchTweetsParameters(value.query)
                {                   
                    Lang = Utils.Mappings.Lang(message.language),
                    SearchType = Utils.Mappings.SearchType(message.resultType),
                    MaximumNumberOfResults = message.maxResultsPerCycle,
                    Since = Utils.Mappings.Since(message.since),
                    Until = Utils.Mappings.Until(message.until)
                };
                if (Utils.Mappings.GeoCode(message.location) != null)
                    searchParameter.GeoCode = Utils.Mappings.GeoCode(message.location);

                var matchingTweets = Search.SearchTweets(searchParameter);
                foreach (var tweet in matchingTweets)
                {
                    DTuriasCore.Models.Tweet message = new DTuriasCore.Models.Tweet()
                    {
                        CreatedAt = tweet.CreatedAt,
                        FavoriteCount = tweet.FavoriteCount,
                        Language = tweet.Language.ToString(),
                        PossiblySensitive = tweet.PossiblySensitive,
                        RetweetCount = tweet.RetweetCount,
                        Text = tweet.Text,
                        Url = tweet.Url,                        
                        User = new DTuriasCore.Models.User()
                        {
                            Name = tweet.CreatedBy.Name,
                            ScreenName = tweet.CreatedBy.ScreenName
                        }
                    };
                    Console.WriteLine(message);
                }
            }


            //while (true)
            //{
            //    _logger.Info("MessageTask");
            //Thread.Sleep(4000);
            //}
        }
    }
}
