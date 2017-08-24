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

        public TweetValuesController(DataContext context, ILoggerFactory logger)
        {
            this.context = context;
            this._logger = logger.CreateLogger("TweetValuesController");
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

            var user = context.Users.FirstOrDefault(u => u.ScreenName == tweetModel.CreatedBy.ScreenName);
            if (user != null)
               tweetModel.CreatedBy = user; //Reusing the same user that is already in the database
            else
               context.Users.Add(tweetModel.CreatedBy); //It is a new user since it is not in the DB

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

    }
}
