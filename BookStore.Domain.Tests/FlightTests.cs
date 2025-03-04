using BookStore.Domain.Services.InMemory;

namespace BookStore.Domain.Tests;

public class FlightTests
{
    [Theory]
    [InlineData(1, 2)]
    [InlineData(2, 3)]
    [InlineData(3, 0)]
    public void GetLast5Booking_Success(int flightId, int expectedCount)
    {
        var repo = new FlightInMemoryRepository();
        var booking = repo.GetLast5Booking(flightId);
        Assert.Equal(expectedCount, booking.Count);
    }

    [Fact]
    public void GetTop5FlightByPageCount_Success()
    {
        var repo = new FlightInMemoryRepository();
        var flight = repo.GetTop5BookingByPageCount();
        Assert.Equal(3, flight.Count);
    }
}