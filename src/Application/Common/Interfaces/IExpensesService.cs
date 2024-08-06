
using ExpensesDemo.Application.Common.DTOs;
using ExpensesDemo.Domain.Entities;

namespace ExpensesDemo.Application.Common.Interfaces;
public interface IExpensesService
{
    Task<IEnumerable<Expense>> GetAll();
    Task<Expense> GetById(int id);
    Task<Expense> Create(ExpenseDto dto);
    Task<Expense> Update(ExpenseDto dto);
    Task Delete(int id);
}
