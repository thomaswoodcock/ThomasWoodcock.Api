using System;
using System.Threading.Tasks;

namespace ThomasWoodcock.Service.Application.Common.Notifications
{
    // TODO: Remove when notifications implemented.
    internal sealed class NotificationSender : INotificationSender
    {
        /// <inheritdoc />
        public Task SendAsync(INotification notification) => throw new NotImplementedException();
    }
}
