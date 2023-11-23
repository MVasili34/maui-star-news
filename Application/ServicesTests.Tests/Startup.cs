using Microsoft.Extensions.DependencyInjection;
using Services;

namespace ServicesTests.Tests;

internal class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IArticleService, ArticleService>();
        services.AddTransient<IAuthUsersService, AuthUsersService>();
        services.AddNewsAppDbContext();
    }
}
