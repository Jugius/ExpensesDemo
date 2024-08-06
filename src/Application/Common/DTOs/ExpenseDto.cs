using ExpensesDemo.Domain.Enums;

namespace ExpensesDemo.Application.Common.DTOs;
public class ExpenseDto
{
    public int Id { get; set; }
    public DateTime PaymentTime { get; set; }
    public ExpenseType Type { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
}
