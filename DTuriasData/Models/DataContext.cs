using Microsoft.EntityFrameworkCore;
using DTuriasCore.Models;

namespace DTuriasData.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TweetModel> Tweets { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PlaceModel> Places { get; set; }
    }
}
