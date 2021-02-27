using System;
using System.Collections.Generic;
using System.Security.Cryptography;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;

using ThomasWoodcock.Service.Application.Common.Cryptography;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;
using ThomasWoodcock.Service.Infrastructure.Cryptography.FailureReasons;

namespace ThomasWoodcock.Service.Infrastructure.Cryptography
{
    /// <inheritdoc />
    /// <summary>
    ///     A concrete implementation of the <see cref="IPasswordHasher" /> interface.
    /// </summary>
    /// <remarks>
    ///     https://github.com/dotnet/AspNetCore/blob/main/src/Identity/Extensions.Core/src/PasswordHasher.cs
    /// </remarks>
    internal sealed class PasswordHasher : IPasswordHasher
    {
        private readonly IRandomNumberGenerator _generator;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PasswordHasher" /> class.
        /// </summary>
        /// <param name="generator">
        ///     The <see cref="IRandomNumberGenerator" /> with which to hash passwords.
        /// </param>
        public PasswordHasher(IRandomNumberGenerator generator)
        {
            this._generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        /// <inheritdoc />
        public string Hash(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            const int saltSize = 128 / 8;
            const KeyDerivationPrf prf = KeyDerivationPrf.HMACSHA256;
            const int iterationCount = 10000;
            const int numBytesRequested = 256 / 8;

            var salt = new byte[saltSize];
            this._generator.GetBytes(salt);
            byte[] subKey = KeyDerivation.Pbkdf2(password, salt, prf, iterationCount, numBytesRequested);

            var outputBytes = new byte[13 + salt.Length + subKey.Length];
            outputBytes[0] = 0x00;

            WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
            WriteNetworkByteOrder(outputBytes, 5, iterationCount);
            WriteNetworkByteOrder(outputBytes, 9, saltSize);

            Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
            Buffer.BlockCopy(subKey, 0, outputBytes, 13 + saltSize, subKey.Length);

            return Convert.ToBase64String(outputBytes);

            static void WriteNetworkByteOrder(IList<byte> buffer, int offset, uint value)
            {
                buffer[offset + 0] = (byte)(value >> 24);
                buffer[offset + 1] = (byte)(value >> 16);
                buffer[offset + 2] = (byte)(value >> 8);
                buffer[offset + 3] = (byte)(value >> 0);
            }
        }

        /// <inheritdoc />
        public IResult Verify(string hashedPassword, string plainPassword)
        {
            if (hashedPassword == null)
            {
                throw new ArgumentNullException(nameof(hashedPassword));
            }

            if (plainPassword == null)
            {
                throw new ArgumentNullException(nameof(plainPassword));
            }

            byte[] passwordBytes = Convert.FromBase64String(hashedPassword);

            var prf = (KeyDerivationPrf)ReadNetworkByteOrder(passwordBytes, 1);
            var iterationCount = (int)ReadNetworkByteOrder(passwordBytes, 5);
            var saltLength = (int)ReadNetworkByteOrder(passwordBytes, 9);

            var salt = new byte[saltLength];
            Buffer.BlockCopy(passwordBytes, 13, salt, 0, salt.Length);

            int subKeyLength = passwordBytes.Length - 13 - salt.Length;
            var expectedSubKey = new byte[subKeyLength];
            Buffer.BlockCopy(passwordBytes, 13 + salt.Length, expectedSubKey, 0, expectedSubKey.Length);

            byte[] actualSubKey = KeyDerivation.Pbkdf2(plainPassword, salt, prf, iterationCount, subKeyLength);

            bool passwordsMatch = CryptographicOperations.FixedTimeEquals(expectedSubKey, actualSubKey);

            return passwordsMatch ? Result.Success() : Result.Failure(new IncorrectPasswordFailure());

            static uint ReadNetworkByteOrder(IReadOnlyList<byte> buffer, int offset) =>
                ((uint)buffer[offset + 0] << 24) | ((uint)buffer[offset + 1] << 16) | ((uint)buffer[offset + 2] << 8) |
                buffer[offset + 3];
        }
    }
}
