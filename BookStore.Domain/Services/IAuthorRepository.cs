using BookStore.Domain.Model;

namespace BookStore.Domain.Services;

public interface IAuthorRepository : IRepository<Flight, int>
{
    IList<Tuple<string, int>> GetTop5BookingByPageCount();
    IList<Tuple<string, int>> GetLast5Booking(int key);
}