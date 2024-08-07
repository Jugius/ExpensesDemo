using ExpensesDemo.WpfUI.Abstract;
using ExpensesDemo.WpfUI.Services;
using ExpensesDemo.WpfUI.Stores;

namespace ExpensesDemo.WpfUI.Commands;
internal class LoadExpensesCommand(DialogProvider dialogProvider, ExpensesStore expensesStore) : AsyncCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        try
        {
            await expensesStore.Load();
        }
        catch (Exception ex)
        {
            dialogProvider.ShowError(ex.Message, "Ошибка загрузки данных");
        }
    }
}
