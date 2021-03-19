using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using ThomasWoodcock.Service.Application.Common.Requests;

namespace ThomasWoodcock.Service.Infrastructure.Requests
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="IPipelineBehavior{TRequest,TResponse}" /> interface used to pass
    ///     <see cref="MediatRRequest{TRequest,TResponse}" /> requests through application request filters.
    /// </summary>
    /// <typeparam name="TRequest">
    ///     The type of application request within the request.
    /// </typeparam>
    /// <typeparam name="TResponse">
    ///     The type of response that is returned.
    /// </typeparam>
    internal sealed class
        MediatRRequestPipelineFilterBehavior<TRequest, TResponse> : IPipelineBehavior<
            MediatRRequest<TRequest, TResponse>, TResponse>
        where TRequest : class, Application.Common.Requests.IRequest<TResponse>
    {
        private readonly IEnumerable<IRequestPipelineFilter<TRequest, TResponse>> _filters;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MediatRRequestPipelineFilterBehavior{TRequest,TResponse}" /> class.
        /// </summary>
        /// <param name="filters">
        ///     The application request filters through which the application request will be passed.
        /// </param>
        public MediatRRequestPipelineFilterBehavior(IEnumerable<IRequestPipelineFilter<TRequest, TResponse>> filters)
        {
            this._filters = filters;
        }

        /// <inheritdoc />
        public async Task<TResponse> Handle(MediatRRequest<TRequest, TResponse> request,
            CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            RequestPipelineDelegate<TResponse> filterChain = this._filters.Reverse()
                .Aggregate((RequestPipelineDelegate<TResponse>)(() => next()),
                    (prev, filter) => () => filter.FilterAsync(request.Request, prev, cancellationToken));

            return await filterChain();
        }
    }
}
