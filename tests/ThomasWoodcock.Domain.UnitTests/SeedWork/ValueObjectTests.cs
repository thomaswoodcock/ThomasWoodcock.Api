using System.Collections.Generic;

using ThomasWoodcock.Domain.SeedWork;

using Xunit;

namespace ThomasWoodcock.Domain.UnitTests.SeedWork
{
    public class ValueObjectTests
    {
        public class EqualOperatorTests
        {
            [Fact]
            public void TwoNullValueObjects_EqualOperator_ReturnsTrue()
            {
                // Arrange
                ValueObject objectOne = null;
                ValueObject objectTwo = null;

                // Act
                bool result = objectOne == objectTwo;

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void TwoValueObjectsWithSamePropertyValues_EqualOperator_ReturnsTrue()
            {
                // Arrange
                var objectOne = new TestValueObject(5);
                var objectTwo = new TestValueObject(5);

                // Act
                bool result = objectOne == objectTwo;

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void TwoValueObjectsWithDifferentPropertyValues_EqualOperator_ReturnsFalse()
            {
                // Arrange
                var objectOne = new TestValueObject(5);
                var objectTwo = new TestValueObject(9);

                // Act
                bool result = objectOne == objectTwo;

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void NullLeftOperand_EqualOperator_ReturnsFalse()
            {
                // Arrange
                TestValueObject objectOne = null;
                var objectTwo = new TestValueObject(5);

                // Act
                bool result = objectOne == objectTwo;

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void NullRightOperand_EqualOperator_ReturnsFalse()
            {
                // Arrange
                var objectOne = new TestValueObject(5);
                TestValueObject objectTwo = null;

                // Act
                bool result = objectOne == objectTwo;

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void ObjectOfDifferentType_EqualOperator_ReturnsFalse()
            {
                // Arrange
                var objectOne = new TestValueObject(5);
                var objectTwo = new AnotherTestValueObject("Test", "Values");

                // Act
                bool result = objectOne == objectTwo;

                // Assert
                Assert.False(result);
            }
        }

        public class NotEqualOperatorTests
        {
            [Fact]
            public void TwoNullValueObjects_NotEqualOperator_ReturnsFalse()
            {
                // Arrange
                ValueObject objectOne = null;
                ValueObject objectTwo = null;

                // Act
                bool result = objectOne != objectTwo;

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void TwoValueObjectsWithSamePropertyValues_NotEqualOperator_ReturnsFalse()
            {
                // Arrange
                var objectOne = new TestValueObject(5);
                var objectTwo = new TestValueObject(5);

                // Act
                bool result = objectOne != objectTwo;

                // Assert
                Assert.False(result);
            }

            [Fact]
            public void TwoValueObjectsWithDifferentPropertyValues_NotEqualOperator_ReturnsTrue()
            {
                // Arrange
                var objectOne = new TestValueObject(5);
                var objectTwo = new TestValueObject(9);

                // Act
                bool result = objectOne != objectTwo;

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void NullLeftOperand_NotEqualOperator_ReturnsTrue()
            {
                // Arrange
                TestValueObject objectOne = null;
                var objectTwo = new TestValueObject(5);

                // Act
                bool result = objectOne != objectTwo;

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void NullRightOperand_NotEqualOperator_ReturnsTrue()
            {
                // Arrange
                var objectOne = new TestValueObject(5);
                TestValueObject objectTwo = null;

                // Act
                bool result = objectOne != objectTwo;

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void ObjectOfDifferentType_NotEqualOperator_ReturnsTrue()
            {
                // Arrange
                var objectOne = new TestValueObject(5);
                var objectTwo = new AnotherTestValueObject("Test", "Values");

                // Act
                bool result = objectOne != objectTwo;

                // Assert
                Assert.True(result);
            }
        }

        public class EqualsTests
        {
            [Fact]
            public void NullObject_Equals_ReturnsFalse()
            {
                // Arrange
                var valueObject = new TestValueObject(5);

                // Act
                bool result = valueObject.Equals(null);

                // Assert
                Assert.False(result);
            }
        }

        public class GetHashCodeTests
        {
            [Fact]
            public void TwoValueObjectsWithSamePropertyValues_GetHashCode_ReturnsSameHashCode()
            {
                // Arrange
                var objectOne = new TestValueObject(5);
                var objectTwo = new TestValueObject(5);

                // Act
                int hashCodeOne = objectOne.GetHashCode();
                int hashCodeTwo = objectTwo.GetHashCode();

                // Assert
                Assert.Equal(hashCodeOne, hashCodeTwo);
            }

            [Fact]
            public void TwoValueObjectsWithDifferentPropertyValues_GetHashCode_ReturnsDifferentHashCodes()
            {
                // Arrange
                var objectOne = new TestValueObject(5);
                var objectTwo = new TestValueObject(9);

                // Act
                int hashCodeOne = objectOne.GetHashCode();
                int hashCodeTwo = objectTwo.GetHashCode();

                // Assert
                Assert.NotEqual(hashCodeOne, hashCodeTwo);
            }

            [Fact]
            public void NullEqualityComponents_GetHashCode_ReturnsZero()
            {
                // Arrange
                var valueObject = new AnotherTestValueObject(null, null);

                // Act
                int result = valueObject.GetHashCode();

                // Assert
                Assert.Equal(0, result);
            }
        }

        public class GetCopyTests
        {
            [Fact]
            public void NoArguments_GetCopy_ReturnsCopyOfValueObjectWithDifferentReference()
            {
                // Arrange
                var valueObject = new TestValueObject(5);

                // Act
                ValueObject result = valueObject.GetCopy();

                // Assert
                Assert.Equal(valueObject, result);
                Assert.False(ReferenceEquals(valueObject, result));
            }
        }

        private class TestValueObject : ValueObject
        {
            public TestValueObject(int testProperty)
            {
                this.TestProperty = testProperty;
            }

            public int TestProperty { get; }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return this.TestProperty;
            }
        }

        private class AnotherTestValueObject : ValueObject
        {
            public AnotherTestValueObject(string testPropertyOne, string testPropertyTwo)
            {
                this.TestPropertyOne = testPropertyOne;
                this.TestPropertyTwo = testPropertyTwo;
            }
            public string TestPropertyOne { get; }
            public string TestPropertyTwo { get; }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return this.TestPropertyOne;
                yield return this.TestPropertyTwo;
            }
        }

    }
}
