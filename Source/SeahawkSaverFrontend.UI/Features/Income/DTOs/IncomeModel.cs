namespace SeahawkSaverFrontend.UI.Features.Income.DTOs;
public class IncomeModel
{
	public Guid IncomeId { get; set; }
	public decimal Amount { get; set; }
	public DateTime? DateTime { get; set; }
}