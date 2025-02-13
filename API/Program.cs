using API.Extension;
using API.Middleware;

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

Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");   //to chek environment
Console.WriteLine($"Database Path: {Path.Combine("", "dating.db")}");   //to check database
Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");
Console.WriteLine($"AppContext Base Directory: {AppContext.BaseDirectory}");

app.Run();

