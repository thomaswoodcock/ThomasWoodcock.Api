using System;

using NSubstitute;

using ThomasWoodcock.Service.Application.Common.Cryptography;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;
using ThomasWoodcock.Service.Infrastructure.Cryptography;
using ThomasWoodcock.Service.Infrastructure.Cryptography.FailureReasons;

using Xunit;

namespace ThomasWoodcock.Service.Infrastructure.UnitTests.Cryptography
{
    public sealed class PasswordHasherTests
    {
        public sealed class Hash : IClassFixture<Fixture>
        {
            private readonly Fixture _fixture;

            public Hash(Fixture fixture)
            {
                this._fixture = fixture;
            }

            [Fact]
            public void NullPassword_Hash_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() => this._fixture.Sut.Hash(null));
            }

            [Fact]
            public void ValidPassword_Hash_ReturnsHashedPassword()
            {
                // Arrange Act
                string password = this._fixture.Sut.Hash("TestPassword123");

                // Assert
                Assert.Equal("AAAAAAEAACcQAAAAEAAAAAAAAAAAAAAAAAAAAAAP8h06W9Os2wfgpssKi2Cle37SeiVnKZhtma/eYAFJRw==",
                    password);
            }
        }

        public sealed class Verify : IClassFixture<Fixture>
        {
            private readonly Fixture _fixture;

            public Verify(Fixture fixture)
            {
                this._fixture = fixture;
            }

            [Fact]
            public void NullHashedPassword_Verify_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() => this._fixture.Sut.Verify(null, "TestPassword123"));
            }

            [Fact]
            public void NullPlainPassword_Verify_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() => this._fixture.Sut.Verify("TestPassword123", null));
            }

            [Fact]
            public void NonMatchingPasswords_Verify_ReturnsFailedResult()
            {
                // Arrange
                string hashedPassword = this._fixture.Sut.Hash("TestPassword123");

                // Act
                IResult result = this._fixture.Sut.Verify(hashedPassword, "DifferentPassword789");

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.IsType<IncorrectPasswordFailure>(result.FailureReason);
            }

            [Fact]
            public void MatchingPasswords_Verify_ReturnsSuccessfulResult()
            {
                // Arrange
                string hashedPassword = this._fixture.Sut.Hash("TestPassword123");

                // Act
                IResult result = this._fixture.Sut.Verify(hashedPassword, "TestPassword123");

                // Assert
                Assert.True(result.IsSuccessful);
                Assert.False(result.IsFailed);
                Assert.Null(result.FailureReason);
            }
        }

        public sealed class Fixture
        {
            private readonly IRandomNumberGenerator _generator = Substitute.For<IRandomNumberGenerator>();

            public Fixture()
            {
                this.Sut = new PasswordHasher(this._generator);
            }

            internal PasswordHasher Sut { get; }
        }
    }
}
