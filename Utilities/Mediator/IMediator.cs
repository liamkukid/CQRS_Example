namespace CQRS_Example.Utilities.Mediator;

public interface IMediator
{
    public Task Send<TCommand>(TCommand command) where TCommand : ICommand;

    public Task Publish<TNotification>(TNotification notification) where TNotification : INotification;
}
