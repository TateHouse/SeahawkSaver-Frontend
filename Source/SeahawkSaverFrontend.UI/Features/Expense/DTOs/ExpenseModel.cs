namespace SeahawkSaverFrontend.UI.Features.Expense.DTOs;
using System.ComponentModel.DataAnnotations;

public class ExpenseModel
{
	public Guid ExpenseId { get; set; }

	[Required]
	public decimal Amount { get; set; }

	[Required]
	public DateTime? DateTime { get; set; }
}