﻿using System.Collections.Generic;
using BookStore.Domain.Model;

namespace BookStore.Domain.Data;

/// <summary>
/// Класс для заполнения коллекций данными
/// </summary>
public static class DataSeeder
{
    // Список рейсов
    public static readonly List<Flight> Flights =
    [
        new()
        {
            Id = 1,
            FlightNumber = "SU123",
            DepartureCity = "Москва",
            ArrivalCity = "Санкт-Петербург",
            AircraftType = "Boeing 737",
            DepartureDate = new System.DateTime(2023, 10, 15, 10, 0, 0),
            ArrivalDate = new System.DateTime(2023, 10, 15, 12, 0, 0)
        },
        new()
        {
            Id = 2,
            FlightNumber = "SU456",
            DepartureCity = "Санкт-Петербург",
            ArrivalCity = "Москва",
            AircraftType = "Airbus A320",
            DepartureDate = new System.DateTime(2023, 10, 15, 14, 0, 0),
            ArrivalDate = new System.DateTime(2023, 10, 15, 16, 0, 0)
        },
        new()
        {
            Id = 3,
            FlightNumber = "SU789",
            DepartureCity = "Москва",
            ArrivalCity = "Сочи",
            AircraftType = "Boeing 777",
            DepartureDate = new System.DateTime(2023, 10, 16, 8, 0, 0),
            ArrivalDate = new System.DateTime(2023, 10, 16, 11, 0, 0)
        }
    ];

    // Список клиентов
    public static readonly List<Customer> Customers =
    [
        new()
        {
            Id = 1,
            Passport = "1234567890",
            FullName = "Иванов Иван Иванович",
            BirthDate = new System.DateTime(1990, 5, 15)
        },
        new()
        {
            Id = 2,
            Passport = "0987654321",
            FullName = "Петров Петр Петрович",
            BirthDate = new System.DateTime(1985, 8, 25)
        },
        new()
        {
            Id = 3,
            Passport = "1122334455",
            FullName = "Сидорова Анна Сергеевна",
            BirthDate = new System.DateTime(1995, 3, 10)
        }
    ];

    // Список бронирований
    public static readonly List<Booking> Bookings =
    [
        new()
        {
            Id = 1,
            FlightId = 1,
            CustomerId = 1,
            TicketNumber = "TICKET123"
        },
        new()
        {
            Id = 2,
            FlightId = 1,
            CustomerId = 1,
            TicketNumber = "TICKET123"
        },
        new()
        {
            Id = 3,
            FlightId = 2,
            CustomerId = 1,
            TicketNumber = "TICKET123"
        },
        new()
        {
            Id = 4,
            FlightId = 2,
            CustomerId = 3,
            TicketNumber = "TICKET123"
        },
        new()
        {
            Id = 5,
            FlightId = 2,
            CustomerId = 3,
            TicketNumber = "TICKET123"
        }
    ];

    static DataSeeder()
    {
        foreach (var booking in Bookings)
        {
            booking.Flight = Flights.FirstOrDefault(f => f.Id == booking.FlightId);
            booking.Customer = Customers.FirstOrDefault(c => c.Id == booking.CustomerId);
        }

        foreach (var flight in Flights)
        {
            flight.Bookings?.AddRange(Bookings.Where(b => b.FlightId == flight.Id));
        }

        foreach (var customer in Customers)
        {
            customer.Bookings?.AddRange(Bookings.Where(b => b.CustomerId == customer.Id));
        }
    }

}