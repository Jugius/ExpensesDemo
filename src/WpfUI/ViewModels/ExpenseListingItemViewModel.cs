
namespace ExpensesDemo.WpfUI.ViewModels;
public class ExpenseListingItemViewModel : ViewModelBase
{
    public int Id { get; set; }
    public string PaymentTime { get; set; }
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
}
