using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Models;

public class Transaction
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 20 characters")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Value is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Value must be a positive number")]
    public decimal Value { get; set; }
    [Required(ErrorMessage = "Date is required")]
    public DateTime Date { get; set; }
    [Required(ErrorMessage = "Category is required")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
