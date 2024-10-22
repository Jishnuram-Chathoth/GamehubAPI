using GamehubAPI.Data;
using GamehubAPI.Data;
using Microsoft.EntityFrameworkCore;
using GamehubAPI.Data;
using GamehubAPI.MyLogging;
using Microsoft.OpenApi.Models;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    //.MinimumLevel.Debug() // Set the minimum logging level
    .WriteTo.Console()    // Optional: log to console
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // Log to file
    .CreateLogger();
// Use Serilog for logging
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddDbContext<GamehubDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("GamehubDBConnection"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GamehubAPI", Version = "v1" });
//});

//builder.Services.AddTransient<IMyLogger, LogToServerMemory>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped(typeof(IGamehubRepository<>), typeof(GamehubRepository<>));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GamehubAPI V1");
    //});
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
