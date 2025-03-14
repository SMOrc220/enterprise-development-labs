﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AirlineBooking.Infrastructure.EfCore;

public class AirlineBookingDbContextFactory : IDesignTimeDbContextFactory<AirlineBookingDbContext>
{
    private const string _connectionString = "Server=localhost;Username=root;Password=root;Database=airlinebooking;Port=3306;Pooling=true;";    
    public AirlineBookingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AirlineBookingDbContext>();
        optionsBuilder.UseMySql(_connectionString, new MySqlServerVersion(new Version(8, 0, 30)));
        return new AirlineBookingDbContext(optionsBuilder.Options);
    }
}