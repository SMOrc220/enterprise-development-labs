namespace BookStore.Domain.Model;

public class Flight
{
    public required int Id { get; set; }
    public required string FlightNumber { get; set; }
    public string? DepartureCity { get; set; }
    public string? ArrivalCity { get; set; }
    public string? AircraftType { get; set; }
    public DateTime? DepartureDate { get; set; }
    public DateTime? ArrivalDate { get; set; }
    public virtual List<Booking>? Bookings { get; set; } = [];
    public int? BookingCount => Bookings?.Count;

    public int? GetFlightCount()
    {
        var sum = 0;
        if (Bookings?.Count > 0)
            foreach (var ba in Bookings)
                if (ba != null && ba.Flight != null)
                    sum += ba.Flight.Id;
        return sum;
    }   
}