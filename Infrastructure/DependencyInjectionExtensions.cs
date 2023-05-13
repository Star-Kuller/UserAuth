using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MyDBConnection");
        
        services.AddDbContext<AppDbContext>(o => 
            o.UseNpgsql(connectionString));

        //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
        
        services.AddTransient<IRepository, Repository>();

        return services;
    }
}