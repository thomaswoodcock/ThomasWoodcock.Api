using System.Threading;
using System.Threading.Tasks;

namespace ThomasWoodcock.Service.Application.Common.Requests
{
    /// <summary>
    ///     Allows a class to act as an application request handler.
    /// </summary>
    /// <typeparam name="TRequest">
    ///     The type of request that the handler handles.
    /// </typeparam>
    /// <typeparam name="TResponse">
    ///     The type of response returned by the handler.
    /// </typeparam>
    public interface IRequestHandler<in TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
    {
        /// <summary>
        ///     Handles the given application request.
        /// </summary>
        /// <param name="request">
        ///     The application request to handle.
        /// </param>
        /// <param name="cancellationToken">
        ///     The cancellation token used to terminate the operation, if requested.
        /// </param>
        /// <returns>
        ///     A response of type <typeparamref name="TResponse" />.
        /// </returns>
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}
