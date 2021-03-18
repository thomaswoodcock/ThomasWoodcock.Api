using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace ThomasWoodcock.Service.Infrastructure.Requests
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="IRequestHandler{TRequest,TResponse}" /> interface used to handle
    ///     <see cref="MediatRRequest{TRequest,TResponse}" /> requests.
    /// </summary>
    /// <typeparam name="TRequest">
    ///     The type of application request within the request.
    /// </typeparam>
    /// <typeparam name="TResponse">
    ///     The type of response that is returned.
    /// </typeparam>
    /// <typeparam name="THandler">
    ///     The application request handler used to handle the application request within the request.
    /// </typeparam>
    internal sealed class
        MediatRRequestHandler<TRequest, TResponse, THandler> : IRequestHandler<MediatRRequest<TRequest, TResponse>,
            TResponse>
        where TRequest : class, Application.Common.Requests.IRequest<TResponse>
        where THandler : Application.Common.Requests.IRequestHandler<TRequest, TResponse>
    {
        private readonly THandler _handler;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MediatRRequestHandler{TRequest,TResponse,THandler}" /> class.
        /// </summary>
        /// <param name="handler">
        ///     The <see cref="Application.Common.Requests.IRequestHandler{TRequest, TResponse}" /> used to handle the application
        ///     request within the request.
        /// </param>
        public MediatRRequestHandler(THandler handler)
        {
            this._handler = handler;
        }

        /// <inheritdoc />
        public async Task<TResponse> Handle(MediatRRequest<TRequest, TResponse> request,
            CancellationToken cancellationToken) =>
            await this._handler.HandleAsync(request.Request, cancellationToken);
    }
}
