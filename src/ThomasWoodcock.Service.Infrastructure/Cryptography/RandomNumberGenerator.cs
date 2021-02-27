using ThomasWoodcock.Service.Application.Common.Cryptography;

using SystemRandomNumberGenerator = System.Security.Cryptography.RandomNumberGenerator;

namespace ThomasWoodcock.Service.Infrastructure.Cryptography
{
    /// <inheritdoc />
    /// <summary>
    ///     A concrete implementation of the <see cref="IRandomNumberGenerator" /> interface.
    /// </summary>
    public sealed class RandomNumberGenerator : IRandomNumberGenerator
    {
        private static readonly SystemRandomNumberGenerator Generator = SystemRandomNumberGenerator.Create();

        /// <inheritdoc />
        public void GetBytes(byte[] bytes) => RandomNumberGenerator.Generator.GetBytes(bytes);
    }
}
