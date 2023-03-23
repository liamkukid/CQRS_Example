namespace CQRS_Example.Application.Validation;

public interface IValidator
{
    Task<ValidationResult> ValidateAsync<TCommand>(TCommand command) where TCommand : IRequest;
}
