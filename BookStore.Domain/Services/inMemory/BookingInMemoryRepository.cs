using BookStore.Domain.Model;
using BookStore.Domain.Data;

namespace BookStore.Domain.Services.InMemory;

public class BookingInMemoryRepository : IRepository<Booking, int>
{
    private List<Booking> _bookings;

    public BookingInMemoryRepository()
    {
        _bookings = DataSeeder.Bookings;
    }

    public bool Add(Booking entity)
    {
        try
        {
            _bookings.Add(entity);
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
            var bookAuthor = Get(key);
            if (bookAuthor != null)
                _bookings.Remove(bookAuthor);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public Booking? Get(int key) =>
        _bookings.FirstOrDefault(item => item.Id == key);

    public IList<Booking> GetAll() =>
        _bookings;

    public bool Update(Booking entity)
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
}