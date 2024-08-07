using ExpensesDemo.Application.Common.DTOs;
using ExpensesDemo.WpfUI.Abstract;
using ExpensesDemo.WpfUI.Services;
using ExpensesDemo.WpfUI.Stores;

namespace ExpensesDemo.WpfUI.Commands;
internal class AddExpenseCommand(DialogProvider dialogProvider, ExpensesStore expensesStore) : AsyncCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        var dto = ExpenseDto.Create();
        if (!dialogProvider.ShowAddEditExpenseDialog(dto)) return;

        try
        {
            await expensesStore.Create(dto);
        }
        catch (Exception ex)
        {
            dialogProvider.ShowError(ex.Message, "Ошибка добавления записи");
        }
    }
}
