using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
