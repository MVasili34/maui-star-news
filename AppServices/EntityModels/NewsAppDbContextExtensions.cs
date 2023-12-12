using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace EntityModels;

public static class NewsAppDbContextExtensions
{
    /// <summary>
    /// Adds <see cref="NewsAppDbContext"/> to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="connectionString">Connection string</param>
    /// <returns><see cref="ServiceCollection"/> to add more services.</returns>
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
