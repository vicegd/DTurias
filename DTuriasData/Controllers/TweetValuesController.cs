using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DTuriasData.Models;
using DTuriasCore.Models;
using Microsoft.Extensions.Logging;
using DTuriasData.Utils;

namespace DTuriasData.Controllers
{
    [Route("api/tweets")]
    public class TweetValuesController : Controller
    {
        private readonly DataContext context;
        private readonly ILogger _logger;

        public TweetValuesController(DataContext context, ILoggerFactory logger)
        {
            this.context = context;
            this._logger = logger.CreateLogger("TweetValuesController");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var tweets = context.Tweets.Select(t => new {
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

            //_logger.LogInformation(LoggingEvents.RESTFulAPI, "TESTING LOGGING");

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
