using ExpensesDemo.Infrastructure.Services;
using ExpensesDemo.WpfUI.Services;
using ExpensesDemo.WpfUI.Stores;
using ExpensesDemo.WpfUI.ViewModels;
using System.IO;
using System.Windows;

namespace ExpensesDemo.WpfUI;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        this.MainWindow = new MainWindow();
        DialogProvider dialogService = new DialogProvider(MainWindow);

        string file = Path.Combine(Environment.CurrentDirectory, "ExpensesDataset.json");
        JsonFileExpensesService expensesService = new JsonFileExpensesService(file);
        ExpensesStore store = new ExpensesStore(expensesService);
        var viewModel = MainWindowVM.LoadViewModel(dialogService, store);
        this.MainWindow.DataContext = viewModel;
        this.MainWindow.Show();
        base.OnStartup(e);
    }
}

