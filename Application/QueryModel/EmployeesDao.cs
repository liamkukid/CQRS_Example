using System.Data;
using Dapper;

namespace CQRS_Example.Application.QueryModel;

public class EmployeesDao : IEmployeesDao
{
    public async Task<EmployeeDisplay> FindAsync(int id)
    {
        using var connection = new SqliteConnection("Data Source=employees.db");
        await connection.OpenAsync();
        var result = await connection.QueryAsync<dynamic>(
        @"
            SELECT FirstName, LastName, Department, JobTitle
            FROM Employees
            WHERE Id = @id
        ", new { id });

        if (result.AsList().Count == 0)
            throw new KeyNotFoundException();

        return MapEmployeeDisplay(result.Single());
    }

    public async Task<ICollection<EmployeeDisplay>> GetAllAsync()
    {
        using var connection = new SqliteConnection("Data Source=employees.db");
        await connection.OpenAsync();
        var result = await connection.QueryAsync<dynamic>(
        @"
            SELECT FirstName, LastName, Department, JobTitle
            FROM Employees
        ");

        List<EmployeeDisplay> emps = new List<EmployeeDisplay>();
        foreach(var item in result) 
        {
            emps.Add(MapEmployeeDisplay(item));
        }
        return emps;
    }

    public async Task<ICollection<string>> GetAllDepartmentsAsync()
    {
        using var connection = new SqliteConnection("Data Source=employees.db");
        await connection.OpenAsync();
        var result = await connection.QueryAsync<dynamic>(
        @"
            SELECT DISTINCT Department
            FROM Employees 
        ");
        List<string> departments = new List<string>();
        foreach (var item in result)
        {
            departments.Add(item.Department);
        }
        return departments;
    }

    public async Task<ICollection<EmployeeDisplay>> GetDepartmentEmployeesAsync(string department)
    {
        using var connection = new SqliteConnection("Data Source=employees.db");
        connection.Open();
        var result = await connection.QueryAsync<dynamic>(
        @"
            SELECT FirstName, LastName, Department, JobTitle
            FROM Employees
            WHERE Department = @department
        ", new { department });

        List<EmployeeDisplay> emps = new List<EmployeeDisplay>();
        foreach (var item in result)
        {
            emps.Add(MapEmployeeDisplay(item));
        }
        return emps;
    }

    public async Task<ManagerAndTeam> GetManagerAndTeamAsync(int managerId)
    {
        using var connection = new SqliteConnection("Data Source=employees.db");
        connection.Open();
        var managerResult = await connection.QueryAsync<dynamic>(
@"
            SELECT FirstName, LastName, Department, JobTitle
            FROM Employees
            WHERE Id = @managerId
        ", new { managerId });

        if (managerResult.AsList().Count == 0)
            throw new KeyNotFoundException();

        var result = await connection.QueryAsync<dynamic>(
        @"
            SELECT FirstName, LastName, Department, JobTitle
            FROM Employees
            WHERE ManagerId = @managerId
        ", new { managerId });

        if (result.AsList().Count == 0)
            throw new KeyNotFoundException();

        List<EmployeeDisplay> emps = new List<EmployeeDisplay>();
        foreach (var item in result)
        {
            emps.Add(MapEmployeeDisplay(item));
        }

        return new ManagerAndTeam()
        {
            Employees = emps,
            Manager = MapEmployeeDisplay(managerResult.Single())
        };
    }

    private EmployeeDisplay MapEmployeeDisplay(dynamic result)
    {
        return new EmployeeDisplay()
        {
            Department = result.Department,
            FirstName = result.FirstName,
            LastName = result.LastName,
            JobTitle = result.JobTitle
        };
    }
}
