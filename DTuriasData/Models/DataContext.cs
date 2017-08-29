using Microsoft.EntityFrameworkCore;
using DTuriasCore.Models;
using System.Linq;
using System.Collections.Generic;

namespace DTuriasData.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }

        public DbSet<DTuriasUserModel> DTuriasUsers { get; set; }
        public DbSet<HashTagModel> HashTags { get; set; }
        public DbSet<PlaceModel> Places { get; set; }
        public DbSet<SentimentModel> Sentiments { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TweetModel> Tweets { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
