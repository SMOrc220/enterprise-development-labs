namespace AirlineBooking.Client.Api;

public interface IAirlineBookingApiWrapper
{
    Task<FlightDto> CreateFlight(FlightCreateUpdateDto newFlight);
    Task<FlightDto> UpdateFlight(int id, FlightCreateUpdateDto updatedFlight);
    Task DeleteFlight(int id);
    Task<FlightDto> GetFlight(int id);
    Task<IList<FlightDto>> GetAllFlights();
    Task<CustomerDto> CreateCustomer(CustomerCreateUpdateDto newCustomer);
    Task<CustomerDto> UpdateCustomer(int id, CustomerCreateUpdateDto updatedCustomer);
    Task DeleteCustomer(int id);
    Task<CustomerDto> GetCustomer(int id);
    Task<IList<CustomerDto>> GetAllCustomers();
    Task<BookingDto> CreateBooking(BookingCreateUpdateDto newBooking);
    Task<BookingDto> UpdateBooking(int id, BookingCreateUpdateDto updatedBooking);
    Task DeleteBooking(int id);
    Task<BookingDto?> GetBooking(int id);
    Task<IList<BookingDto>> GetAllBookings();
    Task<IList<CustomerDto>> GetCustomersByFlight(int flightId);
    Task<IList<FlightDto>> GetFlightsByCustomer(int customerId);
    Task<IList<string>> GetAllFlightsInfo();
    Task<IList<(string FlightNumber, int BookingCount)>> GetTop5FlightsByBookings();
    Task<IList<string>> GetFlightsWithMaxBookings();
    Task<(int Min, double Average, int Max)> GetBookingStatisticsByCity(string departureCity);
    Task<IList<string>> GetFlightsByCityAndDate(string departureCity, DateTime departureDate);
}