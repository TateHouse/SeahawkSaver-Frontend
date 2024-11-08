namespace SeahawkSaverFrontend.UI.Features.Saving.DTOs;
using System.ComponentModel.DataAnnotations;

public class SavingModel
{
	public Guid SavingId { get; set; }

	[Required]
	public decimal Amount { get; set; }

	[Required]
	public DateTime? DateTime { get; set; }
}