namespace BookStore.Domain.Model;

public class Booking
{
    public required int Id { get; set; }
    public required int FlightId { get; set; }
    public virtual Flight? Flight { get; set; }
    public required int CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
    public required string TicketNumber { get; set; }
}