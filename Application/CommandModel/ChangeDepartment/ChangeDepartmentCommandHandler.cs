namespace CQRS_Example.Application.CommandModel.ChangeDepartment;

public class ChangeDepartmentCommandHandler : IRequestHandler<ChangeDepartmentCommand>
{
    private readonly ApplicationDbContext dbContext;

    public ChangeDepartmentCommandHandler(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Handle(ChangeDepartmentCommand request, CancellationToken cancellationToken)
    {
        var employee = await dbContext.Employees.FindAsync(request.EmployeerId);
        employee.ChangeDepartment(request.NewDepartment, request.NewJobTitle);
        await dbContext.SaveEntitiesAsync();
    }
}
