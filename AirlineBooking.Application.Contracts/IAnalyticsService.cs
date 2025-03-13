﻿namespace AirlineBooking.Application.Contracts;

/// <summary>
/// Интерфейс для службы, выполняющей аналитические запросы согласно бизнес-логике приложения
/// </summary>
public interface IFlightAnalyticsService
{
    /// <summary>
    /// Возвращает информацию о всех рейсах в виде списка строк.
    /// </summary>
    /// <returns>Список строк с деталями рейсов.</returns>
    Task<IList<string>> GetAllFlightsInfo();

    /// <summary>
    /// Возвращает список пассажиров для указанного рейса.
    /// </summary>
    /// <param name="flightId">ID рейса.</param>
    /// <returns>Список строк с информацией о пассажирах.</returns>
    Task<IList<string>> GetCustomersByFlight(int flightId);

    /// <summary>
    /// Возвращает рейсы, вылетающие из указанного города в указанную дату.
    /// </summary>
    /// <param name="departureCity">Город вылета.</param>
    /// <param name="date">Дата вылета.</param>
    /// <returns>Список строк с информацией о рейсах.</returns>
    Task<IList<string>> GetFlightsByCityAndDate(string departureCity, DateTime date);

    /// <summary>
    /// Возвращает топ-5 рейсов с наибольшим количеством бронирований.
    /// </summary>
    /// <returns>Список кортежей, содержащих номер рейса и количество бронирований.</returns>
    Task<IList<Tuple<string, int?>>> GetTop5FlightsByBookings();

    /// <summary>
    /// Возвращает рейсы с максимальным количеством бронирований.
    /// </summary>
    /// <returns>Список строк с информацией о рейсах и их бронированиях.</returns>
    Task<IList<string>> GetFlightsWithMaxBookings();

    /// <summary>
    /// Возвращает статистику бронирований для рейсов, вылетающих из указанного города.
    /// </summary>
    /// <param name="departureCity">Город вылета.</param>
    /// <returns>Кортеж с минимальным, средним и максимальным количеством бронирований.</returns>
    Task<(int? Min, double? Average, int? Max)> GetBookingStatisticsByCity(string departureCity);
}