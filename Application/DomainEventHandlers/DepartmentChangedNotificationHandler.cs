namespace CQRS_Example.Application.DomainEventHandlers;

public class DepartmentChangedNotificationHandler : INotificationHandler<DepartmentChangedNotification>
{
    public Task Handle(DepartmentChangedNotification notification, CancellationToken cancellationToken)
    { 
        // 1. Execute domain logic
        // 2. Send integration event to other parts of system
        return Task.CompletedTask;
    }
}
