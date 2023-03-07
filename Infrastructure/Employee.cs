namespace CQRS_Example.Infrastructure;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Department { get; set; }
    public string JobTitle { get; set; }
    public DateTime DateOfEmployment { get; set; }
    public int? ManagerId { get; set; }
}
