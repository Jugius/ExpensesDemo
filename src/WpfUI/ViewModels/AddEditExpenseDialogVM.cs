using ExpensesDemo.Application.Common.DTOs;
using ExpensesDemo.Domain.Enums;
using ExpensesDemo.WpfUI.Abstract;
using ExpensesDemo.WpfUI.Extentions;
using System.Globalization;
using System.Windows;

namespace ExpensesDemo.WpfUI.ViewModels;
internal class AddEditExpenseDialogVM : ViewModelBase
{
    private ExpenseTypeViewModel selectedType;
    private DateTime selectedDate;
    private string selectedTime;
    private string description;
    private decimal amount;

    public string DialogTitle { get; }
    public List<ExpenseTypeViewModel> ExpenseTypes { get; }
    public OKCommand OK { get; }

    public ExpenseTypeViewModel SelectedType { get => selectedType; set => Set(ref selectedType, value); }
    public DateTime SelectedDate { get => selectedDate; set => Set(ref selectedDate, value); }
    public string SelectedTime { get => selectedTime; set => Set(ref selectedTime, value); }
    public string Description { get => description; set => Set(ref description, value); }
    public decimal Amount { get => amount; set => Set(ref amount, value); }

    public AddEditExpenseDialogVM(Window dialogWindow, ExpenseDto expense)
    {
        this.DialogTitle = expense.Id > 0 ? "Редактирование записи" : "Новая запись";
        this.ExpenseTypes = Enum.GetValues<ExpenseType>()
            .Select(a => new ExpenseTypeViewModel(a, a.ToValueString()))
            .ToList();
        this.SelectedType = this.ExpenseTypes.FirstOrDefault(a => a.ExpenseType == expense.Type);
        this.SelectedDate = expense.PaymentTime;
        this.SelectedTime = expense.PaymentTime.ToString("HH:mm");
        this.Description = expense.Description;
        this.Amount = expense.Amount;
        this.OK = new OKCommand(this, expense, dialogWindow);
    }

    public bool IsModelValid()
    {
        try
        {
            if (SelectedType.ExpenseType == ExpenseType.None)
                throw new Exception("Должен быть указан тип затраты");

            if (!DateTime.TryParseExact(SelectedTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                throw new Exception("Время создания записи указано некорректно");

            if(Amount <= 0)
                throw new Exception("Сумма должна быть больше 0");

            if (string.IsNullOrEmpty(Description))
                throw new Exception("Описание очень сильно нужно.");

            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
    }

    public class OKCommand : CommandBase
    {
        private readonly AddEditExpenseDialogVM vm;
        private readonly ExpenseDto dto;
        
        private readonly Window dialogWindow;

        public OKCommand(AddEditExpenseDialogVM vm, ExpenseDto dto, Window dialogWindow)
        {
            this.vm = vm;
            this.dto = dto;
            this.dialogWindow = dialogWindow;
        }

        public override void Execute(object parameter)
        {
            if (vm.IsModelValid())
            {
                UpdateDto(vm, dto);
                this.dialogWindow.DialogResult = true;
                this.dialogWindow.Close();
            }
        }
        private void UpdateDto(AddEditExpenseDialogVM vm, ExpenseDto dto)
        {
            var createdTime = DateTime.ParseExact(vm.SelectedTime, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None);
            dto.PaymentTime = new DateTime(vm.SelectedDate.Year, vm.SelectedDate.Month, vm.SelectedDate.Day, createdTime.Hour, createdTime.Minute, 0);
            dto.Type = vm.SelectedType.ExpenseType;
            dto.Amount = vm.Amount;
            dto.Description = vm.Description;
        }
    }
}
