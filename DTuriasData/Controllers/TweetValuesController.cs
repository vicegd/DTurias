using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DTuriasData.Models;
using DTuriasCore.Models;
using Microsoft.Extensions.Logging;

namespace DTuriasData.Controllers
{
    [Route("api/tweets")]
    public class TweetValuesController : Controller
    {
        private readonly DataContext context;
        private readonly ILogger _logger;

        public TweetValuesController(DataContext context, ILogger<TweetValuesController> logger)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get(int currentPage, int perPage, int sentiment)
        {
            IQueryable<TweetModel> selection;
            if (sentiment == -1)
            {
                selection = context.Tweets;
            }
            else
            {
                selection = context.Tweets
                    .Where(t => t.Sentiment.State == (SentimentModelEnum)sentiment); 
            }

            var tweets = selection
                .OrderBy(row => row.Id)
                .Skip((currentPage-1)*perPage)
                .Take(perPage)
                .Select(t => new {
                    Id = t.Id,
                    Text = t.Text,
                    Sentiment = t.Sentiment.State
                });

            if (tweets == null)
            {
                return NotFound();
            }

            return Ok(tweets);
        }

        [HttpGet("count")]
        public IActionResult GetNumber()
        {
            var number = context.Tweets.Count();
            return Ok(number);
        }

        [HttpGet("sentiment")]
        public IActionResult GetSentiments()
        {
            var both = context.Tweets.Where(t => t.Sentiment.State == SentimentModelEnum.BOTH).Count();
            var negative = context.Tweets.Where(t => t.Sentiment.State == SentimentModelEnum.NEGATIVE).Count();
            var neutral = context.Tweets.Where(t => t.Sentiment.State == SentimentModelEnum.NEUTRAL).Count();
            var positive = context.Tweets.Where(t => t.Sentiment.State == SentimentModelEnum.POSITIVE).Count();
            var undefined = context.Tweets.Where(t => t.Sentiment.State == SentimentModelEnum.UNDEFINED).Count();

            var result = new
            {
                both = both,
                negative = negative,
                neutral = neutral,
                positive = positive,
                undefined = undefined,
            };
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetTweet")]
        public IActionResult GetById(long id)
        {
            var tweet = context.Tweets.FirstOrDefault(t => t.Id == id);

            if (tweet == null)
            {
                return NotFound();
            }

            return new ObjectResult(tweet);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] TweetModel tweetModel)
        {
            if (tweetModel == null)
            {
               return BadRequest();
            }

            var capturedBy = context.Users.FirstOrDefault(u => u.ScreenName == tweetModel.CapturedBy.ScreenName);
            if (capturedBy != null)
                tweetModel.CapturedBy = capturedBy; //Reusing the same user that is already in the database
            else
                context.Users.Add(tweetModel.CapturedBy); //It is a new user since it is not in the DB

            var createdBy = context.Users.FirstOrDefault(u => u.ScreenName == tweetModel.CreatedBy.ScreenName);
            if (createdBy != null)
               tweetModel.CreatedBy = createdBy; //Reusing the same user that is already in the database
            else
               context.Users.Add(tweetModel.CreatedBy); //It is a new user since it is not in the DB

            var dTuriasUser = context.DTuriasUsers.FirstOrDefault(u => u.Nick == tweetModel.DTuriasUser.Nick);
            if (dTuriasUser != null)
                tweetModel.DTuriasUser = dTuriasUser; //Reusing the same user that is already in the database
            else
                context.DTuriasUsers.Add(tweetModel.DTuriasUser); //It is a new user since it is not in the DB

            var sentiment = context.Sentiments.FirstOrDefault(s => s.State == tweetModel.Sentiment.State);
            if (sentiment != null)
                tweetModel.Sentiment = sentiment; //Reusing the same sentiment that is already in the database
            else
                context.Sentiments.Add(tweetModel.Sentiment); //It is a new sentiment since it is not in the DB

            if (tweetModel.Place != null) //The new message has a Place included
            {
                var place = context.Places.FirstOrDefault(p => p.FullName == tweetModel.Place.FullName);
                if (place != null)
                    tweetModel.Place = place; //Reusing the same place that is already in the database
                else
                    context.Places.Add(tweetModel.Place); //It is a new place since it is not in the DB
            }

            context.Tweets.Add(tweetModel);
            context.SaveChanges();
            return Ok(tweetModel.Id);
            //return CreatedAtRoute("GetTweet", new { id = tweetModel.Id }, tweetModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] SentimentModel sentimentValue)
        {
            var tweet = context.Tweets.FirstOrDefault(t => t.Id == id);
            if (tweet == null)
            {
                return NotFound();
            }

            var sentimentInDB = context.Sentiments.FirstOrDefault(s => s.State == sentimentValue.State);
            if (sentimentInDB != null)
            {
                tweet.Sentiment = sentimentInDB;  //Reusing the same sentiment that is already in the database
            }                
            else
            {
                var sentiment = new SentimentModel()
                {
                    State = sentimentValue.State
                };
                context.Sentiments.Add(sentiment);
                tweet.Sentiment = sentiment; //It is a new sentiment since it is not in the DB
            }

            context.Tweets.Update(tweet);
            context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var tweet = context.Tweets.First(t => t.Id == id);
            if (tweet == null)
            {
                return NotFound();
            }

            var hashTags = context.HashTags.Where(t => t.TweetModel.Id == tweet.Id);
            foreach (var hashTag in hashTags)
            {
                context.Remove(hashTag);
            }

            context.Tweets.Remove(tweet);
            context.SaveChanges();
            return new NoContentResult();
        }

    }
}
