namespace CQRS_Example.Utilities;

public interface IMediator
{
    public Task Send<TCommand>(TCommand command) where TCommand : ICommand;
}
