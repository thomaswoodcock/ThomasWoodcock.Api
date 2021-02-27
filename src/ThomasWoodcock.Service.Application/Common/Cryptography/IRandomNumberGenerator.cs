namespace ThomasWoodcock.Service.Application.Common.Cryptography
{
    /// <summary>
    ///     Allows a class to act as a random number generator.
    /// </summary>
    public interface IRandomNumberGenerator
    {
        /// <summary>
        ///     Populates the given <paramref name="bytes" /> with a randomly-generated byte array.
        /// </summary>
        /// <param name="bytes">
        ///     The byte array to populate.
        /// </param>
        void GetBytes(byte[] bytes);
    }
}
