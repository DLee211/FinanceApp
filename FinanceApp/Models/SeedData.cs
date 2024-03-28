using System.Linq;
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
                    Name = "Category1"
                    // other properties
                },

                new Category
                {
                    Name = "Category2"
                    // other properties
                }
            );

            context.SaveChanges();
            
            var categories = context.Category.ToList();
            
            context.Transaction.AddRange(
                new Transaction
                {
                    Name = "Transaction1",
                    Value = 100.0m,
                    Date = DateTime.Now,
                    CategoryId = categories[0].Id // use the Id of the first added category
                    // other properties
                },
                new Transaction
                {
                    Name = "Transaction2",
                    Value = 200.0m,
                    Date = DateTime.Now,
                    CategoryId = categories[1].Id // use the Id of the second added category
                    // other properties
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