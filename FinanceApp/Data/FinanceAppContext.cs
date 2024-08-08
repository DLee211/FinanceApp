using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FinanceApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using IdentityDbContext = Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext;

namespace FinanceApp.Data
{
    public class FinanceAppContext : IdentityDbContext<ApplicationUser>
    {
        public FinanceAppContext (DbContextOptions<FinanceAppContext> options)
            : base(options)
        {
        }
        
        public DbSet<FinanceApp.Models.Category> Category { get; set; } = default!;
        public DbSet<FinanceApp.Models.Transaction> Transaction { get; set; } = default!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";
            
            var client = new IdentityRole("client");
            admin.NormalizedName = "client";
            
            var seller = new IdentityRole("seller");
            seller.NormalizedName = "seller";

            modelBuilder.Entity<IdentityRole>().HasData(admin, client, seller);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinanceAppContext).Assembly);
        
            
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CategoryId)
                .IsRequired(); 

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Transactions)
                .WithOne(t => t.Category)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Value)
                .HasPrecision(18, 4);

            base.OnModelCreating(modelBuilder);
        }
    }
}
