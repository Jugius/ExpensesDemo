using ExpensesDemo.Domain.Common;
using ExpensesDemo.Domain.Enums;

namespace ExpensesDemo.Domain.Entities;
public class Expense : BaseEntity
{
    public DateTime PaymentTime { get; set; }
    public ExpenseType Type { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
}
