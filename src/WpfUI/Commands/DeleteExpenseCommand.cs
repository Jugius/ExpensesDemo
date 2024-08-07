using ExpensesDemo.WpfUI.Abstract;
using ExpensesDemo.WpfUI.Services;
using ExpensesDemo.WpfUI.Stores;
using ExpensesDemo.WpfUI.ViewModels;

namespace ExpensesDemo.WpfUI.Commands;
internal class DeleteExpenseCommand(DialogProvider dialogProvider, ExpensesStore expensesStore) : AsyncCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        const string message = "Вы действительно хотите удалить запись?";
        const string caption = "Удаление записи";

        if (parameter is not ExpenseViewModel model) return;

        if (!dialogProvider.ShowQuestion(message, caption)) return;

        try
        {
            await expensesStore.Delete(model.Id);
        }
        catch (Exception ex)
        {
            dialogProvider.ShowError(ex.Message, "Ошибка удаления записи");
        }
    }
}
