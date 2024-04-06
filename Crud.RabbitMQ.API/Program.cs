using Crud.RabbitMQ.Dominio.DI;
using Crud.RabbitMQ.Infra.DI;
using Serilog;
using Serilog.Events;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Warning()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(@"Log/Log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
try
{
    Log.Warning("Startin up");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    Dependencias.Resolver = new Resolver(builder.Services, builder.Configuration);

    builder.Services.AddMvcCore();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.Warning("Shutting down");
    Log.CloseAndFlush();
}



