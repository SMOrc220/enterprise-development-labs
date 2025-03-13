using AirlineBooking.Domain.Model;
using AirlineBooking.Domain.Services;
using AirlineBooking.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace AirlineBooking.Infrastructure.EfCore.Services;

/// <summary>
/// Реализация репозитория для клиентов в базе данных.
/// </summary>
public class CustomerEfCoreRepository(AirlineBookingDbContext context) : IRepository<Customer, int>
{
    private readonly DbSet<Customer> _customers = context.Customers;

    /// <summary>
    /// Добавляет клиента в базу данных.
    /// </summary>
    public async Task<Customer> Add(Customer entity)
    {
        var result = await _customers.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Удаляет клиента из базы данных по его ID.
    /// </summary>
    public async Task<bool> Delete(int key)
    {
        var entity = await _customers.FirstOrDefaultAsync(e => e.Id == key);
        if (entity == null)
            return false;
        _customers.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Возвращает клиента по его ID.
    /// </summary>
    public async Task<Customer?> Get(int key) =>
        await _customers.FirstOrDefaultAsync(e => e.Id == key);

    /// <summary>
    /// Возвращает всех клиентов из базы данных.
    /// </summary>
    public async Task<IList<Customer>> GetAll() =>
        await _customers.ToListAsync();

    /// <summary>
    /// Обновляет информацию о клиенте в базе данных.
    /// </summary>
    public async Task<Customer> Update(Customer entity)
    {
        _customers.Update(entity);
        await context.SaveChangesAsync();
        return (await Get(entity.Id))!;
    }
}