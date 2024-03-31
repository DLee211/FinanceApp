using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Models;

public class Category
{
    public int Id { get; set; }
    [Microsoft.Build.Framework.Required]
    [StringLength(20, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}