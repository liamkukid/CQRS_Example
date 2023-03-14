namespace CQRS_Example.Application.CommandModel;

public interface ICommand
{
    public Guid Id { get; }
}
