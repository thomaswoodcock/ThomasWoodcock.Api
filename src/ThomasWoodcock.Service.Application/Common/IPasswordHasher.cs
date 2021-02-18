using ThomasWoodcock.Service.Domain.SharedKernel.Results;

namespace ThomasWoodcock.Service.Application.Common
{
    /// <summary>
    ///     Allows a class to act as a password hasher.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        ///     Hashes the given <paramref name="password" />.
        /// </summary>
        /// <param name="password">
        ///     The password to hash.
        /// </param>
        /// <returns>
        ///     The hashed password.
        /// </returns>
        string Hash(string password);

        /// <summary>
        ///     Verifies that the given <paramref name="hashedPassword" /> and <paramref name="plainPassword" /> are equal.
        /// </summary>
        /// <param name="hashedPassword">
        ///     The hashed password with which to match the plain password.
        /// </param>
        /// <param name="plainPassword">
        ///     The plain password to match against the hashed password.
        /// </param>
        /// <returns>
        ///     An <see cref="IResult" /> indicating whether the passwords were verified successfully.
        /// </returns>
        IResult Verify(string hashedPassword, string plainPassword);
    }
}
