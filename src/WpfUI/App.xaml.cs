using ExpensesDemo.Infrastructure.Services;
using ExpensesDemo.WpfUI.Services;
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
        DialogService dialogService = new DialogService(MainWindow);

        string file = Path.Combine(Environment.CurrentDirectory, "ExpensesDataset.json");
        JsonFileExpensesService expensesService = new JsonFileExpensesService(file);
        base.OnStartup(e);
    }
}

