namespace CQRS_Example.CommandModel.ChangeDepartment;

public class ChangeDepartmentCommandHandler : ICommandHandler<ChangeDepartmentCommand>
{
    private readonly ApplicationDbContext dbContext;

    public ChangeDepartmentCommandHandler(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task Handle(ChangeDepartmentCommand command)
    {
        var employee = await dbContext.Employees.FirstAsync(x => x.Id == command.EmployeerId);
        employee.Department = command.NewDepartment;
        employee.JobTitle = command.NewJobTitle;
        await dbContext.SaveChangesAsync();
    }
}
