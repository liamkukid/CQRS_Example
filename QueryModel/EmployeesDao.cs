namespace CQRS_Example.QueryModel
{
    public class EmployeesDao : IEmployeesDao
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesDao(ApplicationDbContext applicationDbContext)
        {
            this.dbContext = applicationDbContext;
        }

        public async Task<ICollection<EmployeeDisplay>> GetAllAsync()
        {
            return await dbContext.Employees.Select(x => Mapper.Map(x)).ToListAsync();
        }

        public async Task<ICollection<string>> GetAllDepartmentsAsync()
        {
            var departments = dbContext.Employees.Select(x => x.Department).Distinct();
            return await departments.ToListAsync();
        }

        public async Task<ICollection<EmployeeDisplay>> GetDepartmentEmployeesAsync(string department)
        {
            return await dbContext.Employees
                .Where(x => x.Department.Equals(department, StringComparison.OrdinalIgnoreCase))
                .Select(x => Mapper.Map(x)).ToListAsync();
        }

        public async Task<ManagerAndTeam> GetManagerAndTeamAsync(int managerId)
        {
            var manager = await dbContext.Employees.SingleOrDefaultAsync(x => x.Id == managerId);
            var employees = await dbContext.Employees
                .Where(x => x.ManagerId == managerId)
                .Select(x => Mapper.Map(x))
                .ToListAsync();
            return new ManagerAndTeam()
            {
                Employees = employees,
                Manager = Mapper.Map(manager)
            };
        }
    }
}
