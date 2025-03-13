using AutoMapper;
using AirlineBooking.Application.Contracts;
using AirlineBooking.Application.Contracts.Booking;
using AirlineBooking.Domain.Model;
using AirlineBooking.Domain.Services;

namespace AirlineBooking.Application.Services;

/// <summary>
/// Служба слоя приложения для манипуляции над бронированиями
/// </summary>
/// <param name="repository">Доменная служба для бронирований</param>
/// <param name="mapper">Автомаппер</param>
public class BookingCrudService(
    IRepository<Booking, int> repository,
    IMapper mapper)
    : ICrudService<BookingDto, BookingCreateUpdateDto, int>
{
    /// <summary>
    /// Создание нового бронирования
    /// </summary>
    public async Task<BookingDto> Create(BookingCreateUpdateDto newDto)
    {
        var newBooking = mapper.Map<Booking>(newDto);
        var res = await repository.Add(newBooking);
        return mapper.Map<BookingDto>(res);
    }

    /// <summary>
    /// Удаляет бронирование по ID.
    /// </summary>
    public async Task<bool> Delete(int id) =>
        await repository.Delete(id);

    /// <summary>
    /// Получение бронирования по ID
    /// </summary>
    public async Task<BookingDto?> GetById(int id)
    {
        var booking = repository.Get(id);
        return mapper.Map<BookingDto>(booking);
    }

    /// <summary>
    /// Получение всех бронирований
    /// </summary>
    public async Task<IList<BookingDto>> GetList() =>
        mapper.Map<List<BookingDto>>(repository.GetAll());

    /// <summary>
    /// Обновление данных о бронировании
    /// </summary>
    public async Task<BookingDto> Update(int key, BookingCreateUpdateDto newDto)
    {
        var newBooking = mapper.Map<Booking>(newDto);
        var res = await repository.Update(newBooking);
        return mapper.Map<BookingDto>(res);
    }
}