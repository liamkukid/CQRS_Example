﻿namespace CQRS_Example.Utilities;

public static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, ApplicationDbContext applicationDbContext)
    {
        var domainEntities = applicationDbContext.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);// as DepartmentChangedNotification);
    }
}