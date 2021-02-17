using ThomasWoodcock.Service.Domain.SharedKernel;
using ThomasWoodcock.Service.Domain.SharedKernel.Results;
using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

using Xunit;

namespace ThomasWoodcock.Service.Domain.UnitTests.SharedKernel
{
    public sealed class EmailAddressTests
    {
        public sealed class Create
        {
            [Fact]
            public void ValidEmailAddress_Create_ReturnsSuccessfulResult()
            {
                // Arrange Act
                IResult<EmailAddress> emailResult = EmailAddress.Create("test@test.com");

                // Assert
                Assert.True(emailResult.IsSuccessful);
                Assert.False(emailResult.IsFailed);
                Assert.Null(emailResult.FailureReason);
                Assert.Equal("test@test.com", emailResult.Value.ToString());
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            [InlineData("test@test.")]
            [InlineData("www.test.com")]
            [InlineData("test^test.com")]
            public void InvalidEmailAddress_Create_ReturnsFailedResult(string emailAddress)
            {
                // Arrange
                IResult<EmailAddress> emailResult = EmailAddress.Create(emailAddress);

                // Assert
                Assert.True(emailResult.IsFailed);
                Assert.False(emailResult.IsSuccessful);
                Assert.IsType<InvalidFormatFailure>(emailResult.FailureReason);
                Assert.Null(emailResult.Value);
            }
        }
    }
}
