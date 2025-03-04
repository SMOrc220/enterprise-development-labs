namespace BookStore.Domain.Model;

public class Customer
{
    public required int Id { get; set; }
    public string? Passport { get; set; }
    public required string FullName { get; set; }
    public DateTime? BirthDate { get; set; }
    public virtual List<Booking>? Bookings { get; set; } = [];
    public int? BookingCount => Bookings?.Count;
}