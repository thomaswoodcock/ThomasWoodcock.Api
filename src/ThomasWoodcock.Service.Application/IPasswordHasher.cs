namespace ThomasWoodcock.Service.Application
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
    }
}
