namespace CQRS_Example.Domain.Events;

public class DepartmentChangedNotification : INotification
{
    public Employee Employee { get; set; }

    public DepartmentChangedNotification()
    {
    }
}
