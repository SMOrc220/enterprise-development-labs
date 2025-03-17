using AirlineBooking.Application.Contracts;
using AirlineBooking.Application.Contracts.Flight;
using AirlineBooking.Domain.Model;
using AirlineBooking.Domain.Services;
using AutoMapper;

namespace AirlineBooking.Application.Services;

/// <summary>
///     Служба слоя приложения для манипуляции над рейсами
/// </summary>
/// <param name="repository">Доменная служба для рейсов</param>
/// <param name="mapper">Автомаппер</param>
public class FlightCrudService(IFlightRepository repository, IMapper mapper)
    : ICrudService<FlightDto, FlightCreateUpdateDto, int>, IFlightAnalyticsService
{
    /// <summary>
    ///     Создание нового рейса
    /// </summary>
    public async Task<FlightDto> Create(FlightCreateUpdateDto newDto)
    {
        Flight? newFlight = mapper.Map<Flight>(newDto);
        Flight res = await repository.Add(newFlight);
        return mapper.Map<FlightDto>(res);
    }

    /// <summary>
    ///     Удаление рейса
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        return await repository.Delete(id);
    }

    /// <summary>
    ///     Получение рейса по ID
    /// </summary>
    public async Task<FlightDto?> GetById(int id)
    {
        Flight? flight = await repository.Get(id);
        return mapper.Map<FlightDto>(flight);
    }

    /// <summary>
    ///     Получение всех рейсов
    /// </summary>
    public async Task<IList<FlightDto>> GetList()
    {
        return mapper.Map<List<FlightDto>>(await repository.GetAll());
    }

    /// <summary>
    ///     Обновление данных о рейсе
    /// </summary>
    public async Task<FlightDto> Update(int key, FlightCreateUpdateDto newDto)
    {
        Flight? newFlight = mapper.Map<Flight>(newDto);
        await repository.Update(newFlight);
        return mapper.Map<FlightDto>(newFlight);
    }

    /// <summary>
    ///     Возвращает информацию о всех рейсах в виде списка строк.
    /// </summary>
    public async Task<IList<string>> GetAllFlightsInfo()
    {
        IList<Flight> flights = await repository.GetAll();
        return flights.Select(flight =>
                $"Рейс: {flight.FlightNumber}, Откуда: {flight.DepartureCity}, Куда: {flight.ArrivalCity}, " +
                $"Дата вылета: {flight.DepartureDate}, Дата прибытия: {flight.ArrivalDate}, Тип самолета: {flight.AircraftType}")
            .ToList();
    }

    /// <summary>
    ///     Возвращает список пассажиров для указанного рейса.
    /// </summary>
    public async Task<IList<string>> GetCustomersByFlight(int flightId)
    {
        Flight? flight = await repository.Get(flightId);
        if (flight == null || flight.Bookings == null)
        {
            return new List<string>();
        }

        return flight.Bookings
            .Where(booking => booking.Customer != null)
            .Select(booking => booking.Customer)
            .OrderBy(customer => customer.FullName)
            .Select(customer =>
                $"Пассажир: {customer.FullName}, Паспорт: {customer.Passport}, Дата рождения: {customer.BirthDate}")
            .ToList();
    }

    /// <summary>
    ///     Возвращает топ-5 рейсов с наибольшим количеством бронирований.
    /// </summary>
    public async Task<IList<Tuple<string, int?>>> GetTop5FlightsByBookings()
    {
        IList<Flight> flights = await repository.GetAll();
        return flights
            .Where(flight => flight.Bookings != null)
            .OrderByDescending(flight => flight.BookingCount)
            .Take(5)
            .Select(flight => new Tuple<string, int?>(flight.FlightNumber, flight.BookingCount))
            .ToList();
    }

    /// <summary>
    ///     Возвращает рейсы с максимальным количеством бронирований.
    /// </summary>
    public async Task<IList<string>> GetFlightsWithMaxBookings()
    {
        IList<Flight> flights = await repository.GetAll();
        var maxBookings = flights
            .Where(flight => flight.Bookings != null)
            .Max(flight => flight.BookingCount);

        return flights
            .Where(flight => flight.Bookings != null && flight.BookingCount == maxBookings)
            .Select(flight =>
                $"Рейс: {flight.FlightNumber}, Откуда: {flight.DepartureCity}, Куда: {flight.ArrivalCity}, " +
                $"Количество бронирований: {flight.BookingCount}")
            .ToList();
    }

    /// <summary>
    ///     Возвращает статистику бронирований для рейсов, вылетающих из указанного города.
    /// </summary>
    public async Task<(int? Min, double? Average, int? Max)> GetBookingStatisticsByCity(string departureCity)
    {
        IList<Flight> flights = await repository.GetAll();
        var bookingsCount = flights
            .Where(flight => flight.DepartureCity == departureCity && flight.Bookings != null)
            .Select(flight => flight.BookingCount)
            .ToList();

        if (bookingsCount.Count == 0)
        {
            return (Min: 0, Average: 0, Max: 0);
        }

        var minBookings = bookingsCount.Min();
        var maxBookings = bookingsCount.Max();
        var averageBookings = bookingsCount.Average();

        return (Min: minBookings, Average: averageBookings, Max: maxBookings);
    }

    /// <summary>
    ///     Возвращает рейсы, вылетающие из указанного города в указанную дату.
    /// </summary>
    public async Task<IList<string>> GetFlightsByCityAndDate(string departureCity, DateTime departureDate)
    {
        IList<Flight> flights = await repository.GetAll();
        return flights
            .Where(flight => flight.DepartureCity == departureCity && flight.DepartureDate == departureDate.Date)
            .Select(flight =>
                $"Рейс: {flight.FlightNumber}, Откуда: {flight.DepartureCity}, Куда: {flight.ArrivalCity}, " +
                $"Дата вылета: {flight.DepartureDate}, Дата прибытия: {flight.ArrivalDate}, Тип самолета: {flight.AircraftType}")
            .ToList();
    }
}