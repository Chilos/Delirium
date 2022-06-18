using Delirium.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Delirium.Modules;

public static class PersistenceModule
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["Database:ConnectionString"];
        services.AddDbContext<DeliriumDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        }, ServiceLifetime.Singleton);
        services.AddSingleton<IDeliriumDbContext>(provider => provider.GetService<DeliriumDbContext>()!);
        return services;
    }
}