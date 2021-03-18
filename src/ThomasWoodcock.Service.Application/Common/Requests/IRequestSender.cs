using System.Threading;
using System.Threading.Tasks;

namespace ThomasWoodcock.Service.Application.Common.Requests
{
    /// <summary>
    ///     Allows a class to act as a sender of application requests.
    /// </summary>
    public interface IRequestSender
    {
        /// <summary>
        ///     Sends the given application request to its respective handler.
        /// </summary>
        /// <param name="request">
        ///     The application request to send.
        /// </param>
        /// <param name="cancellationToken">
        ///     The cancellation token used to terminate the operation, if requested.
        /// </param>
        /// <typeparam name="TResponse">
        ///     The type of response that will be returned.
        /// </typeparam>
        /// <returns>
        ///     A response of type <typeparamref name="TResponse" />.
        /// </returns>
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken);
    }
}
