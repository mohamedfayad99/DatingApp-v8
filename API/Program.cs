using API.Extension;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddData(builder.Configuration);
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var app = builder.Build();
// Configure the HTTP request pipeline.

app.MapControllers();
app.UseCors(x=>x.AllowAnyHeader().AllowAnyMethod()
            .WithOrigins("http://localhost:4200","https://localhost:4200"));
            
app.Run();

