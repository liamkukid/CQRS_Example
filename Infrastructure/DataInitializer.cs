using System.Text.Json;

namespace CQRS_Example.Infrastructure;

public static class DataInitializer
{
    public static async Task InitializeDataAsync(
    ApplicationDbContext context,
    ILogger logger)
    {
        logger.LogInformation("----- Start initializing the data -----");
        var json = await File.ReadAllTextAsync("Infrastructure/employees.json");
        var employees = JsonSerializer.Deserialize<List<Employee>>(json);
        await context.Employees.AddRangeAsync(employees);
        await context.SaveChangesAsync();
        logger.LogInformation("---- The data were initialized ----");
    }
}
