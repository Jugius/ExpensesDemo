using ExpensesDemo.Domain.Entities;
using ExpensesDemo.WpfUI.Abstract;
using ExpensesDemo.WpfUI.Commands;
using ExpensesDemo.WpfUI.Services;
using ExpensesDemo.WpfUI.Stores;
using System.Collections.ObjectModel;


namespace ExpensesDemo.WpfUI.ViewModels;
internal class MainWindowVM : ViewModelBase
{
    private readonly ObservableCollection<ExpenseViewModel> _expenseViewModels;
    private readonly ExpensesStore _expensesStore;
    private ExpenseViewModel selectedExpenseViewModel;

    public IEnumerable<ExpenseViewModel> ExpenseViewModels { get; }
    public ExpenseViewModel SelectedExpenseViewModel { get => selectedExpenseViewModel; set => Set(ref selectedExpenseViewModel, value); }
    public LoadExpensesCommand LoadExpensesCommand { get; }
    public AddExpenseCommand AddExpenseCommand { get; }
    public EditExpenseCommand EditExpenseCommand { get; }
    public DeleteExpenseCommand DeleteExpenseCommand { get; }
    public MainWindowVM(DialogProvider dialogService, ExpensesStore expensesStore)
    {
        _expensesStore = expensesStore;
        _expenseViewModels = new ObservableCollection<ExpenseViewModel>();
        ExpenseViewModels = new ReadOnlyObservableCollection<ExpenseViewModel>(_expenseViewModels);

        AddExpenseCommand = new AddExpenseCommand(dialogService, expensesStore);
        EditExpenseCommand = new EditExpenseCommand(dialogService, expensesStore);
        DeleteExpenseCommand = new DeleteExpenseCommand(dialogService, expensesStore);
        LoadExpensesCommand = new LoadExpensesCommand(dialogService, expensesStore);

        _expenseViewModels.CollectionChanged += ExpenseViewModels_CollectionChanged;

        _expensesStore.ExpensesLoaded += ExpensesStore_ExpensesLoaded;
        _expensesStore.ExpenseAdded += ExpensesStore_ExpenseAdded;
        _expensesStore.ExpenseUpdated += ExpensesStore_ExpenseUpdated;
        _expensesStore.ExpenseDeleted += ExpensesStore_ExpenseDeleted;
    }

    private void ExpenseViewModels_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(ExpenseViewModels));
    }

    private void ExpensesStore_ExpenseDeleted(int id)
    {
        var vm = _expenseViewModels.FirstOrDefault(a => a.Id == id);
        if (vm != null)
        {
            _expenseViewModels.Remove(vm);
        }
    }

    private void ExpensesStore_ExpenseUpdated(Expense expense)
    {
        var vm = _expenseViewModels.FirstOrDefault(a => a.Id == expense.Id);
        if(vm != null)
        {
            vm.Update(expense);
        }
    }

    private void ExpensesStore_ExpenseAdded(Expense expense) => AddExpense(expense);

    private void ExpensesStore_ExpensesLoaded()
    {
        _expenseViewModels.Clear();
        foreach (var expense in _expensesStore.Expenses)
        {
           AddExpense(expense);
        }
    }

    private void AddExpense(Expense expense)
    {
        ExpenseViewModel vm = new ExpenseViewModel(expense);
        _expenseViewModels.Add(vm);
    }


    protected override void Dispose()
    {
        _expensesStore.ExpensesLoaded -= ExpensesStore_ExpensesLoaded;
        _expensesStore.ExpenseAdded -= ExpensesStore_ExpenseAdded;
        _expensesStore.ExpenseUpdated -= ExpensesStore_ExpenseUpdated;
        _expensesStore.ExpenseDeleted -= ExpensesStore_ExpenseDeleted;

        base.Dispose();
    }
    public static MainWindowVM LoadViewModel(DialogProvider dialogService, ExpensesStore expensesStore)
    {
        MainWindowVM viewModel = new MainWindowVM(dialogService, expensesStore);
        viewModel.LoadExpensesCommand.Execute(null);
        return viewModel;
    }
}
