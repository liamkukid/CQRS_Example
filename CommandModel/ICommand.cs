namespace CQRS_Example.CommandModel;

public interface ICommand
{
    public Guid Id { get; }
}
