using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AirlineBooking.Infrastructure.EfCore;

public class AirlineBookingDbContextFactory : IDesignTimeDbContextFactory<AirlineBookingDbContext>
{
    // Переименовываем константу в верхний регистр
    private const string ConnectionString = "Server=localhost;Username=root;Password=new_p@ssw0rd;Database=airlinebooking;Port=3306;Pooling=true;";    
    public AirlineBookingDbContext CreateDbContext(string[] args)
    {
        // Создаем конфигурацию для контекста базы данных
        var optionsBuilder = new DbContextOptionsBuilder<AirlineBookingDbContext>();
        optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));

        // Возвращаем новый экземпляр контекста
        return new AirlineBookingDbContext(optionsBuilder.Options);
    }
}