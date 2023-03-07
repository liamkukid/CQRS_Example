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
            var json1 = await File.ReadAllTextAsync("Infrastructure/employee.json");
            var employee = JsonSerializer.Deserialize<Employee>(json1);


            var json = await File.ReadAllTextAsync("Infrastructure/employees.json");
            var employees = JsonSerializer.Deserialize<List<Employee>>(json);
            await context.Employees.AddRangeAsync(employees);
            await context.SaveChangesAsync();
            logger.LogInformation("The data were initialized");
        }
    }
}
