using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

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

        [HttpGet("ListOfDepartments")]
        public async Task<ActionResult<List<string>>> GetListOfDepartments()
        {
            return await dbContext.Employees.Select(x => x.Department).Distinct().ToListAsync();
        }

        [HttpGet("ManagerAndDirectSubordinates")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetManagerAndDirectSubordinates()
        {
            var manager = await dbContext.Employees.SingleOrDefaultAsync(x => x.JobTitle == "Regional Manager");
            return await dbContext.Employees.Where(x => x.ManagerId == manager.Id).ToListAsync();
        }

        [HttpGet]
        [Route("GetDepartmentEmployeesNames", Name = "departament")]
        public async Task<ActionResult<IEnumerable<string>>> GetDepartmentEmployeesNames(string departament)
        {
            return await dbContext.Employees
                .Where(x => x.Department == departament).Select(x => $"{x.FirstName} {x.LastName}").ToListAsync();
        }
    }
}
