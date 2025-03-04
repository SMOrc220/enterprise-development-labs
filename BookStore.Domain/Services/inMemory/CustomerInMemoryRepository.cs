using BookStore.Domain.Data;
using BookStore.Domain.Model;

namespace BookStore.Domain.Services.InMemory;

public class CustomerInMemoryRepository : IRepository<Customer, int>
{
    private List<Customer> _customers;

    public CustomerInMemoryRepository()
    {
        _customers = DataSeeder.Customers;
    }

    public bool Add(Customer entity)
    {
        try
        {
            _customers.Add(entity);
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
            var book = Get(key);
            if (book != null)
                _customers.Remove(book);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public Customer? Get(int key) =>
        _customers.FirstOrDefault(item => item.Id == key);

    public IList<Customer> GetAll() =>
        _customers;

    public bool Update(Customer entity)
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