using DTuriasCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Tweetinvi.Models;

namespace DTuriasConnectorTwitter.Convertions
{
    class ToTweetModel
    {
        public static TweetModel Convert(ITweet tweet, String dTuriasUserNick)
        {
            if (tweet == null)
            {
                throw new ArgumentNullException(nameof(tweet));
            }

            var message = new TweetModel()
            {
                CreatedAt = tweet.CreatedAt,
                CapturedBy = new UserModel()
                {
                    Name = Tweetinvi.User.GetAuthenticatedUser()?.Name ?? "",
                    ScreenName = Tweetinvi.User.GetAuthenticatedUser()?.ScreenName ?? ""
                },
                Captured = DateTime.Now,
                DTuriasUser = new DTuriasUserModel()
                {
                    Nick = dTuriasUserNick
                },
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
                Sentiment = new SentimentModel()
                {
                    State = SentimentModelEnum.UNDEFINED
                },
                Source = tweet.Source,
                Text = tweet.Text,
                Url = tweet.Url,
                CreatedBy = new UserModel()
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

            return message;
        }
    }
}
