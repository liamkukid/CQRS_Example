namespace CQRS_Example.Utilities.Mediator;

public class Mediator : IMediator
{
    //some kind of storage for processed commands
    private List<Guid> processedCommands = new List<Guid>();

    private readonly IServiceProvider serviceProvider;
    private readonly IValidator validator;

    public Mediator(IServiceProvider serviceProvider, IValidator validator)
    {
        this.serviceProvider = serviceProvider;
        this.validator = validator;
    }

    public async Task Send<TCommand>(TCommand command) where TCommand : ICommand
    {
        var validation = await validator.ValidateAsync(command);
        if (!validation.IsValid)
        {
            throw new ArgumentException(string.Join(";\n\t", validation.Errors));
        }
        if (processedCommands.Contains(command.Id))
        {
            //Here must be the logic for the case when the same command has been handled twice
            //(in case if you are implementing CQRS in a distributed system)
            //...
            return;
        }
        var service = serviceProvider.GetService<ICommandHandler<TCommand>>();
        await service.Handle(command);
    }

    public async Task Publish<TNotification>(TNotification notification) where TNotification : INotification
    {
        var notificationService = serviceProvider.GetService<INotificationHandler<TNotification>>();
        await notificationService.Handle(notification);
    }
}
