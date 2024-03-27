using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinanceApp.Models;

namespace FinanceApp.Data
{
    public class FinanceAppContext : DbContext
    {
        public FinanceAppContext (DbContextOptions<FinanceAppContext> options)
            : base(options)
        {
        }

        public DbSet<FinanceApp.Models.Category> Category { get; set; } = default!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .Property(b => b.Value)
                .HasPrecision(18, 4); // or any precision and scale you need

            base.OnModelCreating(modelBuilder);
        }
    }
}
