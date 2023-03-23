namespace CQRS_Example.Application.CommandModel.ChangeDepartment;

public class ChangeDepartmentCommandHandler : ICommandHandler<ChangeDepartmentCommand>
{
    private readonly ApplicationDbContext dbContext;

    public ChangeDepartmentCommandHandler(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Handle(ChangeDepartmentCommand command)
    {
        var employee = await dbContext.Employees.FindAsync(command.EmployeerId);
        employee.ChangeDepartment(command.NewDepartment, command.NewJobTitle);
        await dbContext.SaveEntitiesAsync();
    }
}
