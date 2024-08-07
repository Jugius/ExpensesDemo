using ExpensesDemo.Application.Common.DTOs;
using System.Windows;

namespace ExpensesDemo.WpfUI.Services;

class DialogProvider
{
    public Window DialogsOwner { get; }
    public DialogProvider(Window dialogsOwner)
    {
        DialogsOwner = dialogsOwner;
    }
    internal void ShowMessage(string message, string caption) =>
        MessageBox.Show(
            messageBoxText: message,
            caption: caption,
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Information);
    internal void ShowError(string message, string caption) =>
        MessageBox.Show(
            messageBoxText: message,
            caption: caption,
            button: MessageBoxButton.OK,
            icon: MessageBoxImage.Error);

    internal bool ShowQuestion(string message, string caption) =>
        MessageBox.Show(
            messageBoxText: message,
            caption: caption,
            button: MessageBoxButton.YesNo,
            icon: MessageBoxImage.Question) == MessageBoxResult.Yes;

    internal bool ShowAddEditExpenseDialog(ExpenseDto dto)
    {
        throw new NotImplementedException();
    }
}
