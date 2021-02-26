using ThomasWoodcock.Service.Application.Common.Commands;

namespace ThomasWoodcock.Service.Application.UnitTests.Common.Commands
{
    internal sealed class CommandStub : ICommand
    {
        public string TestProperty { get; init; }
    }
}
