using ExpensesDemo.Domain.Enums;

namespace ExpensesDemo.WpfUI.Extentions;
internal static class ExpenseTypeExtentions
{
    public static string ToValueString(this ExpenseType type) => type switch
    {
        ExpenseType.HouseholdGoods => "Товары для дома",
        ExpenseType.UtilityBills => "Коммунальные услуги",
        ExpenseType.Groceries => "Продукты",
        ExpenseType.Transportation => "Транспорт",
        _ => throw new Exception($"Не реализован метод ToValueString для ExpenseType {type}.")
    };
}
