namespace CQRS_Example.Application.DomainEventHandlers;

public class DepartmentChangedNotificationHandler : INotificationHandler<DepartmentChangedNotification>
{
    public DepartmentChangedNotificationHandler()
    {
        
    }

    public Task Handle(DepartmentChangedNotification notification)
    {
        // send integration event to other parts of system

        return Task.CompletedTask;
    }
}
