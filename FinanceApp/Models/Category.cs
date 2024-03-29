using Microsoft.Build.Framework;

namespace FinanceApp.Models;

public class Category
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}