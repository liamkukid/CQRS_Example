namespace CQRS_Example.Application.CommandModel.ChangeDepartment;

public class ChangeDepartmentCommand : IRequest
{
    public Guid Id { get; }

    public ChangeDepartmentCommand()
    {
        Id = Guid.NewGuid();
    }

    public int EmployeeId { get; set; }
    public string NewDepartment { get; set; }
    public string NewJobTitle { get; set; }
}
