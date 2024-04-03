using FinanceApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new FinanceAppContext(
                   serviceProvider.GetRequiredService<
                       DbContextOptions<FinanceAppContext>>()))
        {
            // Look for any categories or transactions.
            if (context.Category.Any() || context.Transaction.Any())
            {
                return; // DB has been seeded
            }

            context.Category.AddRange(
                new Category
                {
                    Name = "Groceries"
                },

                new Category
                {
                    Name = "Rent"
                },

                new Category
                {
                    Name = "Utilities"
                },

                new Category
                {
                    Name = "Entertainment"
                },
                
                new Category
                {
                    Name = "Transportation"
                },

                new Category
                {
                    Name = "Healthcare"
                }
            );

            context.SaveChanges();
            
            var categories = context.Category.ToList();
            
            context.Transaction.AddRange(
                new Transaction
                {
                    Name = "Grocery shopping",
                    Value = 150.0m,
                    Date = DateTime.Now,
                    CategoryId = categories[0].Id // use the Id of the Groceries category
                },
                new Transaction
                {
                    Name = "Monthly rent",
                    Value = 1200.0m,
                    Date = DateTime.Now,
                    CategoryId = categories[1].Id // use the Id of the Rent category
                },
                new Transaction
                {
                    Name = "Electricity bill",
                    Value = 80.0m,
                    Date = DateTime.Now,
                    CategoryId = categories[2].Id // use the Id of the Utilities category
                },
                new Transaction
                {
                    Name = "Movie tickets",
                    Value = 30.0m,
                    Date = DateTime.Now,
                    CategoryId = categories[3].Id // use the Id of the Entertainment category
                },
                
                new Transaction
                {
                    Name = "Bus fare",
                    Value = 20.0m,
                    Date = DateTime.Now,
                    CategoryId = categories[4].Id // use the Id of the Transportation category
                },
                
                new Transaction
                {
                    Name = "Doctor's appointment",
                    Value = 200.0m,
                    Date = DateTime.Now,
                    CategoryId = categories[5].Id // use the Id of the Healthcare category
                },
                
                new Transaction
                {
                    Name = "Gym membership",
                    Value = 50.0m,
                    Date = DateTime.Now,
                    CategoryId = categories[3].Id // use the Id of the Entertainment category
                },
                new Transaction
                {
                    Name = "Gas bill",
                    Value = 60.0m,
                    Date = DateTime.Now,
                    CategoryId = categories[2].Id // use the Id of the Utilities category
                }
            );

            context.SaveChanges();
        }
    }

    public static void ClearDatabase(IServiceProvider serviceProvider)
    {
        using (var context = new FinanceAppContext(
                   serviceProvider.GetRequiredService<
                       DbContextOptions<FinanceAppContext>>()))
        {
            // Remove all categories and transactions.
            context.Category.RemoveRange(context.Category);
            context.Transaction.RemoveRange(context.Transaction);

            context.SaveChanges();
        }
    }
}