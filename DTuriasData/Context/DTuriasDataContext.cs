using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DTuriasCore.Models;

namespace DTuriasData.Context
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
