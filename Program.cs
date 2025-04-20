using Microsoft.EntityFrameworkCore;
using PickEm.Api.DataAccess;
using PickEm.Api.Eventing;
using PickEm.Api.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddDbContext<DataContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<IEventEmitter>(sp =>
{
    var eventEmitter = new RabbitMqEmitter("pickem");
    var uri = Environment.GetEnvironmentVariable("RABBITMQ_URI") ?? "localhost";
    eventEmitter.ConnectAsync(uri).Wait();
    return eventEmitter;
});
builder.Services.AddScoped<IFileService, FileSystemFileService>(sp =>
{
    var basePath = Environment.GetEnvironmentVariable("SCHEDULES_PATH") ?? Path.Combine(Directory.GetCurrentDirectory(), "uploads");
    return new FileSystemFileService(basePath);
});
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
app.UsePathBase(Environment.GetEnvironmentVariable("BASE_PATH") ?? string.Empty);
app.MapHealthChecks("/healthz");

// Migrate the database
if (string.Equals(Environment.GetEnvironmentVariable("MIGRATE_DATABASE"), "true", StringComparison.OrdinalIgnoreCase))
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<DataContext>();
        db.Database.Migrate();
    }
}
// Configure the HTTP request pipeline.
if (string.Equals(Environment.GetEnvironmentVariable("ENABLE_SWAGGER"), "true", StringComparison.OrdinalIgnoreCase))
{
    app.MapOpenApi();
}

app.UseCors(options => options.WithOrigins(Environment.GetEnvironmentVariable("ALLOWED_ORIGINS")?.Split(";") ?? ["127.0.0.1:3000"]).AllowAnyHeader().AllowAnyMethod());

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
