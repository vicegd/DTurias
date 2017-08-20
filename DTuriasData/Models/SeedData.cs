using Microsoft.EntityFrameworkCore;
using DTuriasCore.Models;

namespace DTuriasData.Models
{
    public static class SeedData {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();
            context.TodoItems.Add(new TodoItem { Name = "Item1" });
            context.Tweets.Add(new Tweet { Text = "HOLA OH" });
            var u = new User()
            {
                Name = "JJ"
            };
            var t = new Tweet()
            {
                Text = "pruebaaaa",
                User = u
            };
            context.Tweets.Add(t);
            context.Users.Add(new User { Name = "VICENTE" });
            context.Places.Add(new Place { Name = "MADRID " });
            context.SaveChanges();
        }

    }
}
