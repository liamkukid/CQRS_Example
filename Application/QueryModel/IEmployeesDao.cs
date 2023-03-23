namespace CQRS_Example.Application.QueryModel;

public interface IEmployeesDao
{
    Task<ICollection<EmployeeDisplay>> GetAllAsync();
    Task<EmployeeDisplay> FindAsync(int id);
    Task<ICollection<string>> GetAllDepartmentsAsync();
    Task<ManagerAndTeam> GetManagerAndTeamAsync(int managerId);
    Task<ICollection<EmployeeDisplay>> GetDepartmentEmployeesAsync(string department);
}