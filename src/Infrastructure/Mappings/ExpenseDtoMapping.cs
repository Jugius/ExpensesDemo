using ExpensesDemo.Application.Common.DTOs;
using ExpensesDemo.Domain.Entities;

namespace ExpensesDemo.Infrastructure.Mappings;
internal static class ExpenseDtoMapping
{
    public static Expense MapToDomain(this ExpenseDto dto) =>
        new Expense
        {
            Id = dto.Id,
            PaymentTime = dto.PaymentTime,
            Type = dto.Type,
            Amount = dto.Amount,
            Description = dto.Description,
        };

    public static void UpdateWith(this Expense expense, ExpenseDto dto)
    {
        expense.PaymentTime = dto.PaymentTime;
        expense.Type = dto.Type;
        expense.Amount = dto.Amount;
        expense.Description = dto.Description;
    }
}
