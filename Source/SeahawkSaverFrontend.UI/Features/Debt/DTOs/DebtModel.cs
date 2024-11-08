namespace SeahawkSaverFrontend.UI.Features.Debt.DTOs;
using System.ComponentModel.DataAnnotations;

public class DebtModel
{
	public Guid DebtId { get; set; }

	[Required]
	public decimal Amount { get; set; }

	[Required]
	public DateTime? DateTime { get; set; }
}