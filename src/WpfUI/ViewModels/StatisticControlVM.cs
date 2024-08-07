using ExpensesDemo.WpfUI.Abstract;

namespace ExpensesDemo.WpfUI.ViewModels;
internal class StatisticControlVM : ViewModelBase
{
    private readonly MainWindowVM mainWindowVM;
    public bool HasRecords => mainWindowVM.ExpenseViewModels.Any();
    public int CountOfExpenses => mainWindowVM.ExpenseViewModels.Count();
    public decimal SummaOfExpenses => mainWindowVM.ExpenseViewModels.Sum(a => a.Amount);
    public decimal AverageOfExpenses => HasRecords ? mainWindowVM.ExpenseViewModels.Average(a => a.Amount) : 0;
    public decimal MinimumExpense => HasRecords ? mainWindowVM.ExpenseViewModels.Min(a => a.Amount) : 0;
    public decimal MaximumExpense => HasRecords ? mainWindowVM.ExpenseViewModels.Max(a => a.Amount) : 0;


    public StatisticControlVM(MainWindowVM mainWindowVM)
    {
        this.mainWindowVM = mainWindowVM;
        this.mainWindowVM.RecordsCountChanged += MainWindowVM_RecordsCountChanged;
    }

    private void MainWindowVM_RecordsCountChanged()
    {
        OnPropertyChanged(nameof(HasRecords));
        if (HasRecords)
        {
            OnPropertyChanged(nameof(CountOfExpenses));
            OnPropertyChanged(nameof(SummaOfExpenses));
            OnPropertyChanged(nameof(AverageOfExpenses));
            OnPropertyChanged(nameof(MinimumExpense));
            OnPropertyChanged(nameof(MaximumExpense));
        }
    }


    protected override void Dispose()
    {
        this.mainWindowVM.RecordsCountChanged -= MainWindowVM_RecordsCountChanged;
        base.Dispose();
    }
}
