namespace CQRS_Example.Utilities.Mediator;

public interface ICommandHandler<in T> where T : ICommand
{
    Task Handle(T command);
}
