namespace CQRS_Example.Application.DomainEventHandlers;

public class DepartmentChangedNotificationHandler : INotificationHandler<DepartmentChangedNotification>
{
    public Task Handle(DepartmentChangedNotification notification, CancellationToken cancellationToken)
    { 
        // send integration event to other parts of system{
        return Task.CompletedTask;
    }
}
