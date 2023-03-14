namespace CQRS_Example.Utilities.Mediator;

public interface ICommand
{
    public Guid Id { get; }
}
