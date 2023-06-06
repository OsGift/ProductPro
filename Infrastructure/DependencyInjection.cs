using Application.Context;
using Application.Interfaces;
using Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void ConfigureInfraStructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<ApplicationDbContext>();

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

        services.AddTransient<IApplicationSeed, ApplicationSeed>();

        // Seed the data
        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            var seed = scope.ServiceProvider.GetRequiredService<IApplicationSeed>();
            seed.SeedData();
        }
    }

}