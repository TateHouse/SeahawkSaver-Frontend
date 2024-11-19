namespace SeahawkSaverFrontend.UI.Features.Calendar.DTOs;
using System.ComponentModel.DataAnnotations;

public class FinancialEntryModel
{
	public Guid Id { get; set; }

	[Required]
	public decimal Amount { get; set; }

	[Required]
	public DateTime? DateTime { get; set; }

	public string? ErrorMessage { get; set; }

	public FinancialItemType Type { get; set; }
}