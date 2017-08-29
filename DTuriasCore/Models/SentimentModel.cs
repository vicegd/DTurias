using System.ComponentModel.DataAnnotations;

namespace DTuriasCore.Models
{
    public class SentimentModel
    {
        public long Id { get; set; }
        public SentimentModelEnum State { get; set; }

        public override string ToString()
        {
            return $@"
Sentiment: {State}
";
        }
    }

    public enum SentimentModelEnum
    {
        POSITIVE,
        NEGATIVE,
        NEUTRAL,
        BOTH,
        UNDEFINED
    }
}
