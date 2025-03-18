using System.Text;
using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Extension;

public static class Extensions
{
    public static IServiceCollection AddAplicationServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddControllers();
        // var databasePath = Path.Combine(AppContext.BaseDirectory, "dating.db");
        // services.AddDbContext<DataContext>(options =>
        //          options.UseSqlite($"Data Source={databasePath}"));
        //         //  .EnableSensitiveDataLogging()  // Log sensitive data (like SQL queries)
                //  .LogTo(Console.WriteLine)); 

        services.AddDbContext<DataContext>(opt =>{
            opt.UseSqlite(configuration.GetConnectionString("Defaultconnection"));
       });
        services.AddScoped<ITokenServices, TokenServices>();
        services.AddCors();
        services.AddScoped<IUserRepository,Userrepository>();
        services.AddScoped<IPhotoServices,Photoservice>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.Configure<CloudinarySetting>(configuration.GetSection("CloudinarySetting"));
        return services;

    }
    //Middleware
    public static IServiceCollection AddAuthentication( this IServiceCollection services 
            , IConfiguration config) 
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                    var tokenkey=config["TokenKey"]?? throw new Exception("TokenKey not found");
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,  //signing key should be validated
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey)),//the key used to validate the token's signature.
                       ValidateIssuer = false, //the issure of  the token does not need to be validated
                       ValidateAudience = false //the audience of  the token does not need to be validated
                   };
               });
            return services;
        }
}
