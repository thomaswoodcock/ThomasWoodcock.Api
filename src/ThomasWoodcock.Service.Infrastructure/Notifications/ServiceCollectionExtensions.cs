using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using ThomasWoodcock.Service.Application.Common.Notifications;

namespace ThomasWoodcock.Service.Infrastructure.Notifications
{
    /// <summary>
    ///     Notification-related extension methods for <see cref="IServiceCollection" /> objects.
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Registers notification-related services with the given <paramref name="collection" />.
        /// </summary>
        /// <param name="collection">
        ///     The <see cref="IServiceCollection" /> with which to register the services.
        /// </param>
        public static void AddNotifications(this IServiceCollection collection)
        {
            collection.AddSingleton<INotificationSender, NotificationSender>();
        }

        private sealed class NotificationSender : INotificationSender
        {
            /// <inheritdoc />
            public Task SendAsync(INotification notification) => throw new NotImplementedException();
        }
    }
}
