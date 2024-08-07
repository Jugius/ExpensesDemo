using ExpensesDemo.WpfUI.Abstract;

namespace ExpensesDemo.WpfUI.ViewModels;
internal class StatisticControlVM : ViewModelBase
{
    private readonly MainWindowVM mainWindowVM;
    public bool HasRecords => mainWindowVM.ExpenseViewModels.Any();
    public int CountOfExpenses => mainWindowVM.ExpenseViewModels.Count();
    public decimal SummaOfExpenses => mainWindowVM.ExpenseViewModels.Sum(a => a.Amount);
    public decimal AverageOfExpenses => mainWindowVM.ExpenseViewModels.Average(a => a.Amount);
    public decimal MinimumExpense => mainWindowVM.ExpenseViewModels.Min(a => a.Amount);
    public decimal MaximumExpense => mainWindowVM.ExpenseViewModels.Max(a => a.Amount);


    public StatisticControlVM(MainWindowVM mainWindowVM)
    {
        this.mainWindowVM = mainWindowVM;
        this.mainWindowVM.PropertyChanged += MainWindowVM_PropertyChanged;
    }

    private void MainWindowVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "ExpenseViewModels")
        {
            OnPropertyChanged(nameof(HasRecords));
            if(HasRecords)
            {
                OnPropertyChanged(nameof(CountOfExpenses));
                OnPropertyChanged(nameof(SummaOfExpenses));
                OnPropertyChanged(nameof(AverageOfExpenses));
                OnPropertyChanged(nameof(MinimumExpense));
                OnPropertyChanged(nameof(MaximumExpense));
            }            
        }
    }
    protected override void Dispose()
    {
        this.mainWindowVM.PropertyChanged -= MainWindowVM_PropertyChanged;
        base.Dispose();
    }
}
