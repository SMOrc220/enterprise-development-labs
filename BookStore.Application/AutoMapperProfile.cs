using AutoMapper;
using BookStore.Application.Contracts.Flight;
using BookStore.Application.Contracts.Customer;
using BookStore.Application.Contracts.Booking;
using BookStore.Domain.Model;

namespace BookStore.Application;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Flight, FlightDto>();
        CreateMap<FlightCreateUpdateDto, Flight>();

        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerCreateUpdateDto, Customer>();

        CreateMap<Booking, BookingDto>();
        CreateMap<BookingCreateUpdateDto, Booking>();
    }
}