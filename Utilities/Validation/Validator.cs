namespace CQRS_Example.Utilities.Validation;

public class Validator : IValidator
{
    private readonly ApplicationDbContext dbContext;
    private readonly IEmployeesDao employeesDao;

    public Validator(ApplicationDbContext dbContext, IEmployeesDao employeesDao)
    {
        this.dbContext = dbContext;
        this.employeesDao = employeesDao;
    }

    public async Task<ValidationResult> ValidateAsync<TCommand>(TCommand command) 
        where TCommand : ICommand
    {
        switch (command)
        {
            case ChangeDepartmentCommand:
                { 
                    return await ValidateChangeDepartmentCommandAsync(command as ChangeDepartmentCommand);
                }
            default: throw new NotImplementedException($"Validation for \"{nameof(command)}\" command does not implemented");
        }
    }

    private async Task<ValidationResult> ValidateChangeDepartmentCommandAsync(ChangeDepartmentCommand command)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));

        var result = new ValidationResult() { IsValid = false };

        if (string.IsNullOrWhiteSpace(command.NewDepartment) ||
            string.IsNullOrWhiteSpace(command.NewJobTitle) ||
            command.EmployeerId == 0)
        {
            result.Errors.Add("One or more parameters were not assigned");
            return result;
        }

        var employee = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == command.EmployeerId);
        if (employee == null)
        {
            result.Errors.Add("Employee with such id does not exist");
            return result;
        }

        if (employee.Department == "Management")
        {
            result.Errors.Add("To change department for manager use another API command");
        }

        var departments = await employeesDao.GetAllDepartmentsAsync();
        if (!departments.Contains(command.NewDepartment))
        {
            result.Errors.Add("Such department does not exist");
        }

        // ... and other necessary validations...

        result.IsValid = (result.Errors.Count == 0) ? true : false;
        return result;
    }
}
