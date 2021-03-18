using MediatR;

namespace ThomasWoodcock.Service.Infrastructure.Requests
{
    /// <inheritdoc cref="IRequest{TResponse}" />
    /// <summary>
    ///     An implementation of the <see cref="IRequest{TResponse}" /> interface used to send application requests using
    ///     MediatR.
    /// </summary>
    /// <typeparam name="TRequest">
    ///     The type of application request that will be sent.
    /// </typeparam>
    /// <typeparam name="TResponse">
    ///     The type of response that will be returned.
    /// </typeparam>
    /// <param name="Request">
    ///     The application request that will be sent.
    /// </param>
    internal sealed record MediatRRequest<TRequest, TResponse>(TRequest Request) : IRequest<TResponse>
        where TRequest : Application.Common.Requests.IRequest<TResponse>;
}
