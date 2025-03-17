namespace AirlineBooking.Client.Api;

public class AirlineBookingApiWrapper : IAirlineBookingApiWrapper
{
    private readonly AirlineBookingClient _client = new("http://localhost:5225/", new HttpClient());

    #region CRUD Operations

    // Создание рейса
    public async Task<FlightDto> CreateFlight(FlightCreateUpdateDto newFlight)
    {
        return await _client.FlightPOSTAsync(newFlight);
    }

    // Создание пассажира
    public async Task<CustomerDto> CreateCustomer(CustomerCreateUpdateDto newCustomer)
    {
        return await _client.CustomerPOSTAsync(newCustomer);
    }

    // Создание бронирования
    public async Task<BookingDto> CreateBooking(BookingCreateUpdateDto newBooking)
    {
        return await _client.BookingPOSTAsync(newBooking);
    }

    // Обновление рейса
    public async Task<FlightDto> UpdateFlight(int id, FlightCreateUpdateDto updatedFlight)
    {
        return await _client.FlightPUTAsync(id, updatedFlight);
    }

    // Обновление пассажира
    public async Task<CustomerDto> UpdateCustomer(int id, CustomerCreateUpdateDto updatedCustomer)
    {
        return await _client.CustomerPUTAsync(id, updatedCustomer);
    }

    // Обновление бронирования
    public async Task<BookingDto> UpdateBooking(int id, BookingCreateUpdateDto updatedBooking)
    {
        return await _client.BookingPUTAsync(id, updatedBooking);
    }

    // Удаление рейса
    public async Task DeleteFlight(int id)
    {
        await _client.FlightDELETEAsync(id);
    }

    // Удаление пассажира
    public async Task DeleteCustomer(int id)
    {
        await _client.CustomerDELETEAsync(id);
    }

    // Удаление бронирования
    public async Task DeleteBooking(int id)
    {
        await _client.BookingDELETEAsync(id);
    }

    #endregion

    #region Get Operations

    // Получение рейса по ID
    public async Task<FlightDto> GetFlight(int id)
    {
        return await _client.FlightGETAsync(id);
    }

    // Получение пассажира по ID
    public async Task<CustomerDto> GetCustomer(int id)
    {
        return await _client.CustomerGETAsync(id);
    }

    // Получение бронирования по ID
    public async Task<BookingDto?> GetBooking(int id)
    {
        return await _client.BookingGETAsync(id);
    }

    // Получение всех рейсов
    public async Task<IList<FlightDto>> GetAllFlights()
    {
        return [.. await _client.FlightAllAsync()];
    }

    // Получение всех пассажиров
    public async Task<IList<CustomerDto>> GetAllCustomers()
    {
        return [.. await _client.CustomerAllAsync()];
    }

    // Получение всех бронирований
    public async Task<IList<BookingDto>> GetAllBookings()
    {
        return [.. await _client.BookingAllAsync()];
    }

    // Получение всех пассажиров для указанного рейса
    public async Task<IList<CustomerDto>> GetCustomersByFlight(int flightId)
    {
        IList<BookingDto> bookings = await GetAllBookings();
        var customers = new List<CustomerDto>();
        foreach (BookingDto booking in bookings)
        {
            if (booking.FlightId == flightId)
            {
                CustomerDto customer = await GetCustomer(booking.CustomerId);
                if (customer != null)
                {
                    customers.Add(customer);
                }
            }
        }

        return customers;
    }

    // Получение всех рейсов для указанного пассажира
    public async Task<IList<FlightDto>> GetFlightsByCustomer(int customerId)
    {
        IList<BookingDto> bookings = await GetAllBookings();
        var flights = new List<FlightDto>();
        foreach (BookingDto booking in bookings)
        {
            if (booking.CustomerId == customerId)
            {
                FlightDto flight = await GetFlight(booking.FlightId);
                if (flight != null)
                {
                    flights.Add(flight);
                }
            }
        }

        return flights;
    }

    // Получение топ-5 рейсов с наибольшим количеством бронирований
    public async Task<IList<(string FlightNumber, int BookingCount)>> GetTop5FlightsByBookings()
    {
        Console.WriteLine("Получение всех рейсов...");
        IList<FlightDto> flights = await GetAllFlights();
        Console.WriteLine($"Получено {flights?.Count} рейсов.");

        Console.WriteLine("Получение всех бронирований...");
        IList<BookingDto> bookings = await GetAllBookings();
        Console.WriteLine($"Получено {bookings?.Count} бронирований.");

        var result = flights
            .Select(f => (
                f.FlightNumber,
                BookingCount: bookings.Count(b => b.FlightId == f.Id)
            ))
            .OrderByDescending(f => f.BookingCount)
            .Take(5)
            .ToList();

        Console.WriteLine($"Результат: {result}");

        return result;
    }

    public async Task<IList<string>> GetAllFlightsInfo()
    {
        IList<FlightDto> flights = await GetAllFlights();
        return flights.Select(flight =>
                $"Рейс: {flight.FlightNumber}, Откуда: {flight.DepartureCity}, Куда: {flight.ArrivalCity}, " +
                $"Дата вылета: {flight.DepartureDate}, Дата прибытия: {flight.ArrivalDate}, Тип самолета: {flight.AircraftType}")
            .ToList();
    }

    public async Task<IList<string>> GetFlightsByCityAndDate(string departureCity, DateTime departureDate)
    {
        IList<FlightDto> flights = await GetAllFlights();
        return flights
            .Where(flight => flight.DepartureCity == departureCity && flight.DepartureDate == departureDate.Date)
            .Select(flight =>
                $"Рейс: {flight.FlightNumber}, Откуда: {flight.DepartureCity}, Куда: {flight.ArrivalCity}, " +
                $"Дата вылета: {flight.DepartureDate}, Дата прибытия: {flight.ArrivalDate}, Тип самолета: {flight.AircraftType}")
            .ToList();
    }

    public async Task<IList<string>> GetFlightsWithMaxBookings()
    {
        IList<BookingDto> bookings = await GetAllBookings();
        var flightBookingCounts = bookings
            .GroupBy(b => b.FlightId)
            .Select(g => new
            {
                FlightId = g.Key,
                BookingCount = g.Count()
            })
            .ToList();
        var maxBookings = flightBookingCounts.Max(f => f.BookingCount);
        var flightIdsWithMaxBookings = flightBookingCounts
            .Where(f => f.BookingCount == maxBookings)
            .Select(f => f.FlightId)
            .ToList();
        var flightsWithMaxBookings = new List<string>();
        foreach (var flightId in flightIdsWithMaxBookings)
        {
            FlightDto flight = await GetFlight(flightId);
            if (flight != null)
            {
                flightsWithMaxBookings.Add(
                    $"Рейс: {flight.FlightNumber}, Откуда: {flight.DepartureCity}, Куда: {flight.ArrivalCity}, " +
                    $"Количество бронирований: {maxBookings}");
            }
        }

        return flightsWithMaxBookings;
    }

    public async Task<(int Min, double Average, int Max)> GetBookingStatisticsByCity(string departureCity)
    {
        IList<FlightDto> flights = await GetAllFlights();
        IList<BookingDto> bookings = await GetAllBookings();

        var flightBookings = flights
            .Where(f => f.DepartureCity == departureCity)
            .Select(f => bookings.Count(b => b.FlightId == f.Id))
            .ToList();

        var minBookings = flightBookings.Min();
        var maxBookings = flightBookings.Max();
        var averageBookings = flightBookings.Average();

        return (minBookings, averageBookings, maxBookings);
    }

    #endregion
}