using AirlineBooking.Domain.Model;
using AirlineBooking.Domain.Services;
using AirlineBooking.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace AirlineBooking.Infrastructure.EfCore.Services;

/// <summary>
/// Реализация репозитория для бронирований в базе данных.
/// </summary>
public class BookingEfCoreRepository(AirlineBookingDbContext context) : IRepository<Booking, int>
{
    private readonly DbSet<Booking> _bookings = context.Bookings;

    /// <summary>
    /// Добавляет бронирование в базу данных.
    /// </summary>
    public async Task<Booking> Add(Booking entity)
    {
        var result = await _bookings.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Удаляет бронирование из базы данных по его ID.
    /// </summary>
    public async Task<bool> Delete(int key)
    {
        var entity = await _bookings.FirstOrDefaultAsync(e => e.Id == key);
        if (entity == null)
            return false;
        _bookings.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Возвращает бронирование по его ID.
    /// </summary>
    public async Task<Booking?> Get(int key) =>
        await _bookings.FirstOrDefaultAsync(e => e.Id == key);

    /// <summary>
    /// Возвращает все бронирования из базы данных.
    /// </summary>
    public async Task<IList<Booking>> GetAll() =>
        await _bookings.ToListAsync();

    /// <summary>
    /// Обновляет информацию о бронировании в базе данных.
    /// </summary>
    public async Task<Booking> Update(Booking entity)
    {
        _bookings.Update(entity);
        await context.SaveChangesAsync();
        return (await Get(entity.Id))!;
    }
}