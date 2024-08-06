using System.Windows;

namespace ExpensesDemo.WpfUI.Services;

class DialogService
{
    public Window DialogsOwner { get; }
    public DialogService(Window dialogsOwner)
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
}
