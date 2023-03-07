using System.Text.Json;

namespace CQRS_Example.Infrastructure
{
    public static class DataInitializer
    {
        public static async Task InitializeDataAsync(
        ApplicationDbContext context,
        ILogger logger)
        {
            logger.LogInformation("Start initializing the data");
            context.Database.EnsureCreated();
            if (context.Employees.Any())
            {
                return;
            }
            var file = await File.ReadAllTextAsync("employees.json");
            var employees = JsonSerializer.Deserialize<Employee>(file);
            await context.Employees.AddRangeAsync(employees);
            await context.SaveChangesAsync();
            logger.LogInformation("The data were initialized");
        }
    }
}
