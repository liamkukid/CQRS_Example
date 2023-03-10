namespace CQRS_Example.Utilities.Validation;

public interface IValidator
{
    Task<ValidationResult> ValidateAsync<TCommand>(TCommand command) where TCommand : ICommand;
}
