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
        public DbSet<FinanceApp.Models.Transaction> Transaction { get; set; } = default!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .IsRequired(); // Transaction requires a Category

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Transactions)
                .WithOne(t => t.Category)
                .IsRequired(false); // Category does not require a Transaction

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Value)
                .HasPrecision(18, 4); // Specify precision and scale

            base.OnModelCreating(modelBuilder);
        }
    }
}
