using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DTuriasData.Models
{
    public class DTuriasDataContext : DbContext
    {
        public DTuriasDataContext(DbContextOptions<DTuriasDataContext> options)
           : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

    }
}
