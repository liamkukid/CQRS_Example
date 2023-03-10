namespace CQRS_Example.Utilities;

public class Mediator : IMediator
{
    private List<Guid> processedCommands = new List<Guid>();

    private readonly IServiceProvider serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task Send<TCommand>(TCommand command) where TCommand : ICommand
    {
        if(processedCommands.Contains(command.Id))
        {
            //Here must be the logic for the case when the same command has been handled twice
            //(in case if you are implementing CQRS in a distributed system)
            //...
            return;
        }
        var service = serviceProvider.GetService<ICommandHandler<TCommand>>();
        await service.Handle(command);
    }
}
