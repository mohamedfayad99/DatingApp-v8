using API.Data;
using API.Extension;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddAplicationServices(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();  
app.UseCors(x=>x.AllowAnyHeader().AllowAnyMethod()
            .WithOrigins("http://localhost:4200","https://localhost:4200"));
            
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
using var scope=app.Services.CreateScope();
var services=scope.ServiceProvider;
try{
    var context=services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUser(context);

}catch(Exception ex){
    var logger=services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex,"An Error occured  during migration");

}

// Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");   //to chek environment
// Console.WriteLine($"Database Path: {Path.Combine("", "dating.db")}");   //to check database
// Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");
// Console.WriteLine($"AppContext Base Directory: {AppContext.BaseDirectory}");

app.Run();

