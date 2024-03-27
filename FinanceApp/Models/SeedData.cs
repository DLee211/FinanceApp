using FinanceApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new FinanceAppContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<FinanceAppContext>>());
        SeedCategories(context);

        using var context1 = new FinanceAppContext1(
            serviceProvider.GetRequiredService<
                DbContextOptions<FinanceAppContext1>>());
        SeedTransactions(context1, serviceProvider);
    }

    private static void SeedCategories(FinanceAppContext context)
    {
        // Look for any categories.
        if (context.Category.Any())
        {
            return;   // DB has been seeded
        }

        context.Category.AddRange(
            new Category
            {
                Name = "Groceries"
            },

            new Category
            {
                Name = "Utilities"
            }
        );

        context.SaveChanges();
    }

    private static void SeedTransactions(FinanceAppContext1 context, IServiceProvider serviceProvider)
    {
        // Look for any transactions.
        if (context.Transaction.Any())
        {
            return;   // DB has been seeded
        }

        using var context2 = new FinanceAppContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<FinanceAppContext>>());

        var category1 = context2.Category.FirstOrDefault(c => c.Name == "Groceries");
        var category2 = context2.Category.FirstOrDefault(c => c.Name == "Utilities");

        if (category1 == null)
        {
            category1 = new Category { Name = "Groceries" };
            context2.Category.Add(category1);
            context2.SaveChanges();
        }

        if (category2 == null)
        {
            category2 = new Category { Name = "Utilities" };
            context2.Category.Add(category2);
            context2.SaveChanges();
        }

        context.Transaction.AddRange(
            new Transaction
            {
                Date = DateTime.Now,
                Value = 100.50m,
                CategoryId = category1.Id,
                Name = "Transaction 1"
            },

            new Transaction
            {
                Date = DateTime.Now.AddDays(-1),
                Value = 200.75m,
                CategoryId = category2.Id,
                Name = "Transaction 2"
            }
        );

        context.SaveChanges();
    }
    
    public static void ClearDatabase(IServiceProvider serviceProvider)
    {
        using var context1 = new FinanceAppContext1(
            serviceProvider.GetRequiredService<
                DbContextOptions<FinanceAppContext1>>());
        ClearTransactions(context1);

        using var context = new FinanceAppContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<FinanceAppContext>>());
        ClearCategories(context);
    }

    private static void ClearTransactions(FinanceAppContext1 context)
    {
        foreach (var transaction in context.Transaction)
        {
            context.Transaction.Remove(transaction);
        }

        context.SaveChanges();
    }

    private static void ClearCategories(FinanceAppContext context)
    {
        foreach (var category in context.Category)
        {
            context.Category.Remove(category);
        }

        context.SaveChanges();
    }
}