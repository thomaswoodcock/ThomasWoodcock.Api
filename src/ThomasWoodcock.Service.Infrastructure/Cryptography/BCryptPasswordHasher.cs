using ThomasWoodcock.Service.Application.Common.Cryptography;

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
        public bool Verify(string hashedPassword, string plainPassword) => BC.Verify(plainPassword, hashedPassword);
    }
}
