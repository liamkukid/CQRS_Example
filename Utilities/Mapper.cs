namespace CQRS_Example.Utilities;

public static class Mapper
{
    public static EmployeeDisplay Map(Employee employee)
    {
        return new EmployeeDisplay()
        {
            Department = employee.Department,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            JobTitle = employee.JobTitle
        };
    }
}
