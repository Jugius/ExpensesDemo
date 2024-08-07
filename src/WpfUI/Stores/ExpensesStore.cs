using ExpensesDemo.Application.Common.DTOs;
using ExpensesDemo.Application.Common.Interfaces;
using ExpensesDemo.Domain.Entities;

namespace ExpensesDemo.WpfUI.Stores;
internal class ExpensesStore
{
    private readonly List<Expense> _expenses;
    private readonly IExpensesService _expensesService;
    private Lazy<Task> _initializeLazy;

    public IEnumerable<Expense> Expenses => _expenses;

    public event Action ExpensesLoaded;
    public event Action<Expense> ExpenseAdded;
    public event Action<Expense> ExpenseUpdated;
    public event Action<int> ExpenseDeleted;

    public ExpensesStore(IExpensesService expensesService)
    {
        _expensesService = expensesService;
        _initializeLazy = new Lazy<Task>(Initialize);
        _expenses = new List<Expense>();
    }

    public async Task Create(ExpenseDto dto)
    {
        var expense = await _expensesService.Create(dto);
        _expenses.Add(expense);
        this.ExpenseAdded?.Invoke(expense);
    }

    public async Task Update(ExpenseDto dto)
    {
        var expense = await _expensesService.Update(dto);
        int currentIndex = _expenses.FindIndex(y => y.Id == expense.Id);

        if (currentIndex != -1)
        {
            _expenses[currentIndex] = expense;
        }
        else
        {
            _expenses.Add(expense);
        }

        ExpenseUpdated?.Invoke(expense);
    }
    public async Task Delete(int Id)
    {
        await _expensesService.Delete(Id);
        _expenses.RemoveAll(y => y.Id == Id);
        ExpenseDeleted?.Invoke(Id);
    }

    public async Task Load()
    {
        try
        {
            await _initializeLazy.Value;
        }
        catch (Exception)
        {
            _initializeLazy = new Lazy<Task>(Initialize);
            throw;
        }
    }
    private async Task Initialize()
    {
        IEnumerable<Expense> expenses = await _expensesService.GetAll();

        _expenses.Clear();
        _expenses.AddRange(expenses);

        this.ExpensesLoaded?.Invoke();
    }


}
