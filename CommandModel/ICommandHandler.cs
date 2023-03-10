namespace CQRS_Example.CommandModel;

public interface ICommandHandler<in T> where T : ICommand
{
    public Task Handle(T command);
}
