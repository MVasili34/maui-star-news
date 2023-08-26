using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EntityModels;

public static class NewsAppDbContextExtensions
{
    /// <summary>
    /// Метод расширения для добавления контекста базы данных в
    /// коллекцию сервисов зависимостей, применяя IServiceCollection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="connectionString"></param>
    /// <returns>ServiceCollection для добавления других сервисов</returns>
    public static IServiceCollection AddNewsAppDbContext(
        this IServiceCollection services, 
        string connectionString = "Host=localhost;Port=5432;Database=NewsAppDB;" +
        "Username=postgres;Password=sqlserver")
    {
        services.AddDbContext<NewsAppDbContext>(options => 
        options.UseNpgsql(connectionString));
        return services;
    }
}
