using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Text.RegularExpressions;

namespace CQRS_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmploeeysAsync()
        {
            return await dbContext.Employees.ToListAsync();
        }

        [HttpGet("Departments")]
        public async Task<ActionResult<List<string>>> Departments()
        {
            return await dbContext.Employees.Select(x => x.Department).Distinct().ToListAsync();
        }

        [HttpGet("ManagerAndSubordinates")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetManagerAndSubordinates()
        {
            var manager = await dbContext.Employees.SingleOrDefaultAsync(x => x.JobTitle == "Regional Manager");
            return await dbContext.Employees.Where(x => x.ManagerId == manager.Id).ToListAsync();
        }

        [HttpGet]
        [Route("GetDepartmentEmployees", Name = "departament")]
        public async Task<ActionResult<IEnumerable<string>>> GetDepartmentEmployees(string departament)
        {
            bool isValid = Regex.IsMatch(departament, @"^[a-zA-Z]+$");
            if (!isValid) return BadRequest();
            return await dbContext.Employees
                .Where(x => x.Department.Equals(departament, StringComparison.OrdinalIgnoreCase)).Select(x => $"{x.FirstName} {x.LastName}").ToListAsync();
        }
    }
}
