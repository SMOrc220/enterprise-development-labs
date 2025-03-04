using BookStore.Domain.Model;
using BookStore.Domain.Data;

namespace BookStore.Domain.Services.InMemory;

public class FlightInMemoryRepository : IAuthorRepository
{
    private List<Flight> _flights;

    public FlightInMemoryRepository()
    {
        _flights = DataSeeder.Flights;
    }

    public bool Add(Flight entity)
    {
        try
        {
            _flights.Add(entity);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public bool Delete(int key)
    {
        try
        {
            var author = Get(key);
            if (author != null)
                _flights.Remove(author);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public bool Update(Flight entity)
    {
        try
        {
            Delete(entity.Id);
            Add(entity);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public Flight? Get(int key) =>
        _flights.FirstOrDefault(item => item.Id == key);

    public IList<Flight> GetAll() =>
        _flights;

    public IList<Tuple<string, int>> GetLast5Booking(int key)
    {
        var flight = Get(key);
        var customers = new List<Customer>();
        if (flight != null && flight.Bookings?.Count > 0)
            foreach (var bs in flight.Bookings)
                if (bs.Customer != null)
                    customers.Add(bs.Customer);
        return customers
            .OrderByDescending(customer => customer.FullName)
            .Take(5)
            .Select(customer => new Tuple<string, int>(customer.FullName, customer.BookingCount ?? 0))
            .ToList();
    }


    public IList<Tuple<string, int>> GetTop5BookingByPageCount() =>
        _flights
            .OrderByDescending(flights => flights.GetFlightCount() ?? 0)
            .Take(5)
            .Select(flights => new Tuple<string, int>(flights.FlightNumber, flights.GetFlightCount() ?? 0))
            .ToList();
}