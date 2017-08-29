using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DTuriasCore.Models
{
    public class TweetModel
    {
        public long Id { get; set; }
       // [ForeignKey("DTuriasUserModel")]
        public DTuriasUserModel DTuriasUser { get; set; }
        [DataType(DataType.DateTime)]
       // [ForeignKey("UserModel")]
        public UserModel CapturedBy { get; set; }
        public DateTime CreatedAt { get; set; }
       // [ForeignKey("UserModel")]
        public UserModel CreatedBy { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Captured { get; set; }
        public PlaceModel Place { get; set; }
        public CoordinateModel Coordinates { get; set; }
        public int FavoriteCount { get; set; }
        public bool Favorited { get; set; }
        public string FullText { get; set; }
        public List<HashTagModel> HashTags { get; set; }
        public bool IsRetweet { get; set; }
        public bool IsTweetDestroyed { get; set; }
        public bool IsTweetPublished { get; set; }
        public string Language { get; set; }
        public long QuotedTweet { get; set; }
        public bool PossiblySensitive { get; set; }
        public int RetweetCount { get; set; }
        public bool Retweeted { get; set; }
        public long RetweetedTweet { get; set; }
        [ForeignKey("SentimentModel")]
        public SentimentModel Sentiment { get; set; }
        public string Source { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }

        public override string ToString()
        {
            return $@"     
Text: {Text}
    FullText: {FullText}
    DTuriasUser: {DTuriasUser.Nick}
    Source: {Source}
    CreatedAt: {CreatedAt} - Captured: {Captured} by {CapturedBy.ScreenName}
    Language: {Language}
    HashTags: {PrintHashTags()}
    IsTweetPublished: {IsTweetPublished} - IsTweetDestroyed: {IsTweetDestroyed}
    Favorited: {Favorited} - FavoriteCount: {FavoriteCount}
    Retweeted: {Retweeted} - RetweetCount: {RetweetCount} - RetweetedTweet: {RetweetedTweet}
    IsRetweet: {IsRetweet} - QuotedTweet {QuotedTweet}
    PossiblySensitive: {PossiblySensitive}
    Url: {Url}
    User: {CreatedBy.ScreenName} ({CreatedBy.Name})
    Place: {Place?.FullName ?? "-"} ({Place?.Country ?? "-"})
    Coordinates: ({Coordinates?.Latitude ?? 0.0}, {Coordinates?.Longitude ?? 0.0})
    Sentiment: {Sentiment.State}
";
        }

        public string PrintHashTags()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (var hashTag in HashTags)
            {
                sb.Append(hashTag.Text + ", ");
            }
            sb.Append("]");
            return sb.ToString();
        }

    }
}
