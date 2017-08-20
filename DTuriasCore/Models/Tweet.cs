using System;

namespace DTuriasCore.Models
{
    public class Tweet
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FavoriteCount { get; set; }
        public string Language { get; set; }
        public bool PossiblySensitive { get; set; }
        public int RetweetCount { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public User User { get; set; }

        public override string ToString()
        {
            return $@"     
Text: {Text}
   CreatedAt: {CreatedAt}
   Language: {Language}
   FavoriteCount: {FavoriteCount} - RetweetCount: {RetweetCount}
   PossiblySensitive: {PossiblySensitive}
   Url: {Url}
   User: {User.ScreenName} ({User.Name})
";
        }
    }
}
