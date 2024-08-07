using ExpensesDemo.Application.Mappings;
using ExpensesDemo.WpfUI.Abstract;
using ExpensesDemo.WpfUI.Services;
using ExpensesDemo.WpfUI.Stores;
using ExpensesDemo.WpfUI.ViewModels;

namespace ExpensesDemo.WpfUI.Commands;
internal class EditExpenseCommand(DialogProvider dialogProvider, ExpensesStore expensesStore) : AsyncCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        if (parameter is not ExpenseViewModel model) return;
        var dto = model.Expense.MapToDto();
        
        if (!dialogProvider.ShowAddEditExpenseDialog(dto)) return;

        try
        {
            await expensesStore.Update(dto);
        }
        catch (Exception ex)
        {
            dialogProvider.ShowError(ex.Message, "Ошибка обновления записи");
        }
    }
}
