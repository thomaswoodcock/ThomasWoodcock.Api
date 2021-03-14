using ThomasWoodcock.Service.Application.Accounts.Entities;
using ThomasWoodcock.Service.Application.Common.Notifications;
using ThomasWoodcock.Service.Domain.Accounts;

namespace ThomasWoodcock.Service.Application.Accounts.Notifications
{
    /// <inheritdoc cref="INotification" />
    /// <summary>
    ///     An implementation of the <see cref="INotification" /> interface used for notifying users that their account needs
    ///     to be activated.
    /// </summary>
    /// <param name="Account">
    ///     The account that requires activation.
    /// </param>
    /// <param name="ActivationKey">
    ///     The key that can be used to activate the account.
    /// </param>
    internal sealed record AccountActivationNotification
        (Account Account, AccountActivationKey ActivationKey) : INotification;
}
