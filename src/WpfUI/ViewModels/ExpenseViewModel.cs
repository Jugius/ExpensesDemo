using ExpensesDemo.Domain.Entities;
using ExpensesDemo.WpfUI.Abstract;
using ExpensesDemo.WpfUI.Extentions;

namespace ExpensesDemo.WpfUI.ViewModels;
public class ExpenseViewModel : ViewModelBase
{
    private Expense _expense;
    public Expense Expense => _expense;
    public int Id => _expense.Id;
    public string PaymentTime => _expense.PaymentTime.ToString("dd.MM.yy HH:mm");
    public string Type => _expense.Type.ToValueString();
    public decimal Amount => _expense.Amount;
    public string Description => _expense.Description;
    public ExpenseViewModel(Expense expense)
    {
        _expense = expense;
    }
    
    public void Update(Expense expense)
    {
        _expense = expense;
        OnPropertyChanged(nameof(PaymentTime));
        OnPropertyChanged(nameof(Type));
        OnPropertyChanged(nameof(Amount));
        OnPropertyChanged(nameof(Description));
    }
}
