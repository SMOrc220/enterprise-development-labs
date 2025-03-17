namespace AirlineBooking.Application.Contracts.Booking;

/// <summary>
///     Dto для просмотра сведений о бронировании
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="CustomerId">Идентификатор клиента</param>
/// <param name="FlightId">Идентификатор рейса</param>
/// <param name="TicketNumber">Номер билета</param>
public record BookingDto(
    int Id,
    int CustomerId,
    int FlightId,
    string TicketNumber
);