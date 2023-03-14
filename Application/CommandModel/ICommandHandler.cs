namespace CQRS_Example.Application.CommandModel;

public interface ICommandHandler<in T> where T : ICommand
{
    public Task Handle(T command);
}
