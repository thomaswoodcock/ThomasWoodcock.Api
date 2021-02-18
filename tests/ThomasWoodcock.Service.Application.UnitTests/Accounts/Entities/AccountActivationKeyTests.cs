using System;

using ThomasWoodcock.Service.Application.Accounts.Entities;

using Xunit;

namespace ThomasWoodcock.Service.Application.UnitTests.Accounts.Entities
{
    public sealed class AccountActivationKeyTests
    {
        public sealed class Constructor
        {
            [Fact]
            public void EmptyValue_Constructor_ThrowsArgumentNullException()
            {
                // Arrange Act Assert
                Assert.Throws<ArgumentNullException>(() => new AccountActivationKey(Guid.Empty));
            }

            [Fact]
            public void ValidValue_Constructor_SetsValue()
            {
                // Arrange
                Guid value = new("86301369-9E0A-43F0-868B-906132B90B88");

                // Act
                AccountActivationKey sut = new(value);

                // Assert
                Assert.Equal(value, sut.Value);
            }
        }
    }
}
