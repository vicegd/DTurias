using DTuriasConnectorTwitter.Config;
using DTuriasConnectorTwitter.Convertions;
using DTuriasConnectorTwitter.Http;
using DTuriasCore.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Threading;
using Tweetinvi;
using Tweetinvi.Parameters;

namespace DTuriasConnectorTwitter.Tasks
{
    class TweetTask
    {
        static ILog _logger = LogManager.GetLogger(typeof(TweetTask));
        MessagesToSearch message;
        Manager manager;

        public TweetTask(Manager manager, MessagesToSearch message)
        {
            this.manager = manager;
            this.message = message;
        }

        public void Run()
        {
            _logger.Info($"Initially sleeping for {message.startIn} MS");
            Thread.Sleep(message.startIn);
            var timeCounter = message.startIn;

            while (timeCounter <= message.endsIn)
            {
                foreach (var value in message.queries)
                {
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
                        var message = ToTweetModel.Convert(tweet, manager.DTuriasUserNick);
                        Console.WriteLine(message);
                        manager.CreateTweet(message);
                    }
                } //Foreach

                _logger.Info($"Sleeping for {message.every} MS");
                timeCounter += message.every;
                Thread.Sleep(message.every);
            } //while
            _logger.Info("Finished");
        }
    }
}
