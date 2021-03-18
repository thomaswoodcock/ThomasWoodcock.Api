using ThomasWoodcock.Service.Application.Common.Requests;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Common.Commands
{
    /// <inheritdoc />
    /// <summary>
    ///     Allows a class to act as an application command.
    /// </summary>
    internal interface ICommand : IRequest<IResult>
    {
    }
}
