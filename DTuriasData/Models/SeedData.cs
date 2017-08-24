using Microsoft.EntityFrameworkCore;
using DTuriasCore.Models;
using System;

namespace DTuriasData.Models
{
    public static class SeedData {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();

            TweetModel tweet = new TweetModel()
            {
                CreatedAt = new System.DateTime(),
                FavoriteCount = 10,
                Language = "English",
                PossiblySensitive = true,
                RetweetCount = 25,
                Text = "Este es un Tweet de ejemplo",
                Url = "http://www.google.com"
            };

            UserModel user = new UserModel()
            {
                Name = "Vicente",
                ScreenName = "viegd"
            };

            tweet.CreatedBy = user;
            context.Users.Add(user);

            TweetModel tweet2 = new TweetModel()
            {
                CreatedAt = DateTime.Now,
                FavoriteCount = 10,
                Language = "English",
                PossiblySensitive = true,
                RetweetCount = 25,
                Text = "Este es un Tweet de ejemplo",
                Url = "http://www.google.com"
            };

            tweet2.CreatedBy = user;

            /* context.TodoItems.Add(new TodoItem { Name = "Item1" });
             context.Tweets.Add(new Tweet { Text = "HOLA OH" });
             var u = new User()
             {
                 Name = "JJ"
             };
             var t = new Tweet()
             {
                 Text = "pruebaaaa",
                 User = u
             };*/
            context.Tweets.Add(tweet);
            /*context.Users.Add(new User { Name = "VICENTE" });
            //context.Places.Add(new Place { Name = "MADRID " });*/
            context.SaveChanges();
        }

    }
}
