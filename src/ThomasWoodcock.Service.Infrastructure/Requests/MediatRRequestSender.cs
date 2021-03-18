using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using ThomasWoodcock.Service.Application.Common.Requests;

namespace ThomasWoodcock.Service.Infrastructure.Requests
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="IRequestSender" /> interface used to send application requests using MediatR.
    /// </summary>
    internal sealed class MediatRRequestSender : IRequestSender
    {
        private readonly ISender _sender;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MediatRRequestSender" /> class.
        /// </summary>
        /// <param name="sender">
        ///     The sender used to send MediatR requests.
        /// </param>
        public MediatRRequestSender(ISender sender)
        {
            this._sender = sender;
        }

        /// <inheritdoc />
        public async Task<TResponse> SendAsync<TResponse>(Application.Common.Requests.IRequest<TResponse> request,
            CancellationToken cancellationToken)
        {
            MediatR.IRequest<TResponse> req = CreateRequest(request);

            return await this._sender.Send(req, cancellationToken);

            static MediatR.IRequest<TResponse> CreateRequest(Application.Common.Requests.IRequest<TResponse> request)
            {
                Type requestType = typeof(MediatRRequest<,>).MakeGenericType(request.GetType(), typeof(TResponse));

                return (MediatR.IRequest<TResponse>?)Activator.CreateInstance(requestType, request) ??
                       throw new InvalidOperationException();
            }
        }
    }
}
