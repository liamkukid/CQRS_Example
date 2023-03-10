namespace CQRS_Example.QueryModel;

public record ManagerAndTeam
{
    public EmployeeDisplay Manager { get; set; }
    public ICollection<EmployeeDisplay> Employees { get; set;}
}
