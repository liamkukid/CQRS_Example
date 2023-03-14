namespace CQRS_Example.Application.QueryModel;

public record EmployeeDisplay
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Department { get; set; }
    public string JobTitle { get; set; }
}
