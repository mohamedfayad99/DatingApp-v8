using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extension;

public static class Extensions
{
    public static IServiceCollection AddData(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(opt =>{
            opt.UseSqlite(configuration.GetConnectionString("Defaultconnection"));
        });
        return services;

    }
}
