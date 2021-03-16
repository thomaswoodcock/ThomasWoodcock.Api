using ThomasWoodcock.Service.Application.Common.Cryptography;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;
using ThomasWoodcock.Service.Infrastructure.Cryptography.FailureReasons;

using BC = BCrypt.Net.BCrypt;

namespace ThomasWoodcock.Service.Infrastructure.Cryptography
{
    /// <inheritdoc />
    /// <summary>
    ///     An implementation of the <see cref="IPasswordHasher" /> interface that uses the bcrypt hashing function.
    /// </summary>
    internal sealed class BCryptPasswordHasher : IPasswordHasher
    {
        /// <inheritdoc />
        public string Hash(string password) => BC.HashPassword(password);

        /// <inheritdoc />
        public IResult Verify(string hashedPassword, string plainPassword)
        {
            bool passwordsMatch = BC.Verify(plainPassword, hashedPassword);

            return passwordsMatch ? Result.Success() : Result.Failure(new IncorrectPasswordFailure());
        }
    }
}
