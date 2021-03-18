using System.Threading.Tasks;

namespace ThomasWoodcock.Service.Application.Common.Notifications
{
    /// <summary>
    ///     Allows a class to act as a service that sends notifications to users.
    /// </summary>
    public interface INotificationSender
    {
        /// <summary>
        ///     Sends a notification.
        /// </summary>
        /// <param name="notification">
        ///     The notification to send.
        /// </param>
        Task SendAsync(INotification notification);
    }
}
