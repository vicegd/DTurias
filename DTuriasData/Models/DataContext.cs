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
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Place> Places { get; set; }
    }
}
