using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CQRS_Example.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeesDao employeesDao;
    private readonly IMediator mediator;

    public EmployeesController(IEmployeesDao employeesDao, IMediator mediator)
    {
        this.employeesDao = employeesDao;
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<ICollection<EmployeeDisplay>> GetEmployeesAsync()
    {
        return await employeesDao.GetAllAsync();
    }

    [HttpGet]
    [Route("GetEmployee", Name = "id")]
    public async Task<EmployeeDisplay> GetEmployee(int id)
    {
        return await employeesDao.FindAsync(id);
    }


    [HttpGet("Departments")]
    public async Task<ICollection<string>> Departments()
    {
        return await employeesDao.GetAllDepartmentsAsync();
    }

    [HttpGet]
    [Route("GetManagerAndTeam", Name = "managerId")]
    public async Task<ManagerAndTeam> GetManagerAndTeam(int managerId)
    {
        return await employeesDao.GetManagerAndTeamAsync(managerId);
    }

    [HttpGet]
    [Route("GetDepartmentEmployees", Name = "department")]
    public async Task<ActionResult<ICollection<EmployeeDisplay>>> GetDepartmentEmployees(string department)
    {
        bool isValid = Regex.IsMatch(department, @"^[a-zA-Z]+$");
        if (!isValid) return BadRequest();
        var employees =  await employeesDao.GetDepartmentEmployeesAsync(department);
        return Ok(employees);
    }

    [HttpPost]
    public async Task<ActionResult> ChangeDepartment(
        int employeeId, string newDepartmentName, string newJobTitle)
    {
        var command = new ChangeDepartmentCommand()
        {
            EmployeeId = employeeId,
            NewDepartment = newDepartmentName,
            NewJobTitle = newJobTitle
        };
        await mediator.Send(command);
        return Ok();
    }
}
