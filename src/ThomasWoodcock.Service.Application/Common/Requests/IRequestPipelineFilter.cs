using System.Threading;
using System.Threading.Tasks;

namespace ThomasWoodcock.Service.Application.Common.Requests
{
    /// <summary>
    ///     Executes the next action in the request pipeline.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of response returned by the next action.
    /// </typeparam>
    public delegate Task<T> RequestPipelineDelegate<T>();

    /// <summary>
    ///     Allows a class to act as a filter in application request pipelines.
    /// </summary>
    /// <typeparam name="TRequest">
    ///     The type of request that is handled by the pipeline.
    /// </typeparam>
    /// <typeparam name="TResponse">
    ///     The type of response that is returned from the pipeline.
    /// </typeparam>
    public interface IRequestPipelineFilter<in TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
    {
        /// <summary>
        ///     Filters the incoming request.
        /// </summary>
        /// <param name="request">
        ///     The request that will be filtered.
        /// </param>
        /// <param name="next">
        ///     The next action in the request pipeline.
        /// </param>
        /// <param name="cancellationToken">
        ///     The cancellation token used to terminate the operation, if requested.
        /// </param>
        /// <returns>
        ///     A response of type <typeparamref name="TResponse" />.
        /// </returns>
        Task<TResponse> FilterAsync(TRequest request, RequestPipelineDelegate<TResponse> next,
            CancellationToken cancellationToken);
    }
}
