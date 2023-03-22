using CQRS_Example.Application.DomainEventHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.EnableSensitiveDataLogging();
    options.UseInMemoryDatabase("Employees_DB");
});

builder.Services.AddScoped(
    typeof(ICommandHandler<ChangeDepartmentCommand>), 
    typeof(ChangeDepartmentCommandHandler));

builder.Services.AddScoped(
    typeof(INotificationHandler<DepartmentChangedNotification>), 
    typeof(DepartmentChangedNotificationHandler));

builder.Services.AddScoped<IEmployeesDao, EmployeesDao>();
builder.Services.AddScoped<IValidator, Validator>();
builder.Services.AddScoped<IMediator, Mediator>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await DataInitializer.InitializeDataAsync(context, logger);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "---- An error occurred creating the DB. ----");
    }
}

app.UseAuthorization();

app.MapControllers();

app.Run();
