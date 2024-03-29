var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.EnableSensitiveDataLogging();
    options.UseSqlite("Data Source=employees.db");
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(ChangeDepartmentCommand).Assembly);
});

builder.Services.AddScoped<IEmployeesDao, EmployeesDao>();
builder.Services.AddScoped<IValidator, Validator>();

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
