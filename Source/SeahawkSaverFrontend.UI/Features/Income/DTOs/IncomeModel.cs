namespace SeahawkSaverFrontend.UI.Features.Income.DTOs;
using System.ComponentModel.DataAnnotations;

public class IncomeModel
{
	public Guid IncomeId { get; set; }

	[Required]
	public decimal Amount { get; set; }

	[Required]
	public DateTime? DateTime { get; set; }
}