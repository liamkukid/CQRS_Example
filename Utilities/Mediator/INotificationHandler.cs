namespace CQRS_Example.Utilities.Mediator;

public interface INotificationHandler<in T> where T : INotification
{
    Task Handle(T notification);
}
