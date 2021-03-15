using ThomasWoodcock.Service.Domain.SharedKernel.Results;
using ThomasWoodcock.Service.Domain.SharedKernel.Results.FailureReasons;

using Xunit;

namespace ThomasWoodcock.Service.Domain.UnitTests.SharedKernel.Results
{
    public sealed class ResultTests
    {
        public sealed class Success
        {
            [Fact]
            public void NoValue_Success_ReturnsSuccessfulResult()
            {
                // Arrange Act
                IResult result = Result.Success();

                // Assert
                Assert.True(result.IsSuccessful);
                Assert.False(result.IsFailed);
                Assert.Null(result.FailureReason);
            }
        }

        public sealed class TypedSuccess
        {
            [Fact]
            public void Value_TypedSuccess_ReturnsSuccessfulResult()
            {
                // Arrange Act
                IResult<string> result = Result.Success("Test Result");

                // Assert
                Assert.True(result.IsSuccessful);
                Assert.False(result.IsFailed);
                Assert.Null(result.FailureReason);
                Assert.Equal("Test Result", result.Value);
            }
        }

        public sealed class Failure
        {
            [Fact]
            public void FailureReason_Failure_ReturnsFailedResult()
            {
                // Arrange
                TestFailureReason failureReason = new();

                // Act
                IResult result = Result.Failure(failureReason);

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.Equal(failureReason, result.FailureReason);
            }
        }

        public sealed class TypedFailure
        {
            [Fact]
            public void FailureReason_TypedFailure_ReturnsFailedResult()
            {
                // Arrange
                TestFailureReason failureReason = new();

                // Act
                IResult<string> result = Result.Failure<string>(failureReason);

                // Assert
                Assert.True(result.IsFailed);
                Assert.False(result.IsSuccessful);
                Assert.Equal(failureReason, result.FailureReason);
                Assert.Null(result.Value);
            }
        }

        private sealed class TestFailureReason : IFailureReason
        {
        }
    }
}
