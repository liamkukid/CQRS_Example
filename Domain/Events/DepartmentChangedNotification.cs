namespace CQRS_Example.Domain.Events;

public class DepartmentChangedNotification : INotification
{
    public Employee Employee { get; }

    public DepartmentChangedNotification(Employee employee)
    {
        Employee = employee;
    }
}
