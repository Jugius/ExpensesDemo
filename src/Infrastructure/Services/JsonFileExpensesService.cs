using ExpensesDemo.Application.Common.DTOs;
using ExpensesDemo.Application.Common.Interfaces;
using ExpensesDemo.Application.Mappings;
using ExpensesDemo.Domain.Entities;

namespace ExpensesDemo.Infrastructure.Services;
public class JsonFileExpensesService : IExpensesService
{
    private readonly string _filePath;
    private List<Expense> _buffered;

    public JsonFileExpensesService(string jsonFilePath)
    {
        _filePath = jsonFilePath;
    }

    public async Task<IEnumerable<Expense>> GetAll()
    {
        return await GetOrInitialize();
    }

    public async Task<Expense> GetById(int id)
    {
        var allRecords = await GetOrInitialize();
        return allRecords.FirstOrDefault(a => a.Id == id);
    }

    public async Task<Expense> Create(ExpenseDto dto)
    {
        var expense = dto.MapToDomain();
        var allRecords = await GetOrInitialize(); 

        expense.Id = allRecords.Count > 0
            ? allRecords.Max(x => x.Id) + 1
            : 1;

        _buffered.Add(expense);

        await SaveToFile(_buffered, _filePath);
        return expense;
    }
    public async Task<Expense> Update(ExpenseDto dto)
    {
        _ = await GetOrInitialize();
        var expense = _buffered.FirstOrDefault(x => x.Id == dto.Id);

        if (expense == null)
            throw new Exception($"Запись Id={dto.Id} не найдена.");

        expense.UpdateWith(dto);

        await SaveToFile(_buffered, _filePath);
        return expense;
    }

    public async Task Delete(int id)
    {
        _ = await GetOrInitialize();
        var expense = _buffered.FirstOrDefault(x => x.Id == id);

        if (expense == null)
            throw new Exception($"Запись Id={id} не найдена.");

        _buffered.Remove(expense);

        await SaveToFile(_buffered, _filePath);
    }
    private async ValueTask<List<Expense>> GetOrInitialize()
    {
        if (_buffered != null)
            return _buffered;

        _buffered = await LoadFromFile(_filePath);

        return _buffered;
    }
    private static async Task<List<Expense>> LoadFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            using (var stream = File.OpenRead(filePath))
            {
                return await System.Text.Json.JsonSerializer.DeserializeAsync<List<Expense>>(stream);
            }
        }

        return new List<Expense>();
    }
    private static async Task SaveToFile(List<Expense> expenses, string filePath)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        using (var stream = File.Create(filePath))
        {
            await System.Text.Json.JsonSerializer.SerializeAsync(stream, expenses);
        }
    }    
}
