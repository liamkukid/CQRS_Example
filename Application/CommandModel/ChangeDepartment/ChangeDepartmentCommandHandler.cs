namespace CQRS_Example.Application.CommandModel.ChangeDepartment;

public class ChangeDepartmentCommandHandler : IRequestHandler<ChangeDepartmentCommand>
{
    private readonly ApplicationDbContext dbContext;

    public ChangeDepartmentCommandHandler(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Handle(ChangeDepartmentCommand command, CancellationToken cancellationToken)
    {
        var employee = await dbContext.Employees.FindAsync(command.EmployeeId);
        employee.ChangeDepartment(command.NewDepartment, command.NewJobTitle);
        await dbContext.SaveEntitiesAsync();
    }
}
