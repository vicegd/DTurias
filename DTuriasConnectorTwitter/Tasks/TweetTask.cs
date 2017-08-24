using DTuriasConnectorTwitter.Config;
using DTuriasConnectorTwitter.Http;
using DTuriasCore.Models;
using log4net;
using System;
using System.Collections.Generic;
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
                    var message = new TweetModel()
                    {
                        CreatedAt = tweet.CreatedAt,
                        Captured = DateTime.Now,
                        FavoriteCount = tweet.FavoriteCount,
                        Favorited = tweet.Favorited,
                        FullText = tweet.FullText,
                        IsRetweet = tweet.IsRetweet,
                        IsTweetDestroyed = tweet.IsTweetDestroyed,
                        IsTweetPublished = tweet.IsTweetPublished,
                        Language = tweet.Language.ToString(),
                        PossiblySensitive = tweet.PossiblySensitive,
                        QuotedTweet = tweet?.QuotedTweet?.Id ?? -1L,
                        RetweetCount = tweet.RetweetCount,
                        Retweeted = tweet.Retweeted,
                        RetweetedTweet = tweet?.RetweetedTweet?.Id ?? -1L,
                        Source = tweet.Source,
                        Text = tweet.Text,
                        Url = tweet.Url,                        
                        CreatedBy = new DTuriasCore.Models.UserModel()
                        {
                            Name = tweet.CreatedBy.Name,
                            ScreenName = tweet.CreatedBy.ScreenName
                        },
                    };

                    if (tweet.Coordinates != null)
                    {
                        message.Coordinates = new CoordinateModel()
                        {
                            Latitude = tweet.Coordinates.Latitude,
                            Longitude = tweet.Coordinates.Longitude
                        };
                    }

                    message.HashTags = new List<HashTagModel>();
                    if (tweet.Hashtags != null)
                    {
                        foreach (var hashtag in tweet.Hashtags)
                        {
                            var hashTagModel = new HashTagModel()
                            {
                                Text = hashtag.Text,
                            };
                            message.HashTags.Add(hashTagModel);
                        }
                    }

                    if (tweet.Place != null)
                    {
                        message.Place = new PlaceModel()
                        {
                            Country = tweet.Place.Country,
                            CountryCode = tweet.Place.CountryCode,
                            Name = tweet.Place.Name,
                            FullName = tweet.Place.FullName
                        };
                    }

                    Console.WriteLine(message);
                    manager.CreateTweet(message);
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
