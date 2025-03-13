using AutoMapper;
using AirlineBooking.Application;
using AirlineBooking.Application.Contracts.Flight;
using AirlineBooking.Application.Contracts.Customer;
using AirlineBooking.Application.Contracts.Booking;
using AirlineBooking.Application.Services;
using AirlineBooking.Domain.Services;
using AirlineBooking.Domain.Model;
using AirlineBooking.Domain.Services.InMemory;
using System.Reflection;
using AirlineBooking.Application.Contracts;
using AirlineBooking.Infrastructur.EfCores.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(CustomerDto).Assembly.GetName().Name}.xml"));
});

var mapperConfig = new MapperConfiguration(config => config.AddProfile(new AutoMapperProfile()));
IMapper? mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddTransient<IRepository<Flight, int>, FlightEfCoreRepository>();
builder.Services.AddTransient<IRepository<Customer, int>, CustomerEfCoreRepository>();
builder.Services.AddTransient<IRepository<Booking, int>, BookingEfCoreRepository>();

builder.Services.AddScoped<ICrudService<FlightDto, FlightCreateUpdateDto, int>, FlightCrudService>();
builder.Services.AddScoped<ICrudService<CustomerDto, CustomerCreateUpdateDto, int>, CustomerCrudService>();
builder.Services.AddScoped<ICrudService<BookingDto, BookingCreateUpdateDto, int>, BookingCrudService>();

builder.Services.AddScoped<IFlightAnalyticsService, FlightCrudService>();
builder.Services.AddSingleton<IFlightRepository, FlightInMemoryRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5244");
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();