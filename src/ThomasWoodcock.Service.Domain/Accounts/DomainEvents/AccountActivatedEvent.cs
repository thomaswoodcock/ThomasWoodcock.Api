using ThomasWoodcock.Service.Domain.SeedWork;

namespace ThomasWoodcock.Service.Domain.Accounts.DomainEvents
{
    /// <inheritdoc cref="IDomainEvent" />
    /// <summary>
    ///     An implementation of the <see cref="IDomainEvent" /> interface that represents a domain event that is raised when
    ///     an account has been activated.
    /// </summary>
    /// <param name="Account">
    ///     The account that was activated.
    /// </param>
    public sealed record AccountActivatedEvent(Account Account) : IDomainEvent;
}
