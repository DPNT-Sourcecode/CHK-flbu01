﻿using BeFaster.App.Solutions.HLO;
using FluentAssertions;

namespace BeFaster.App.Tests.Solutions.HLO
{
    public class HelloSolutionTest
    {
        [Theory]
        [InlineData("John", "Hello, John!")]
        [InlineData("", "Hello, !")]
        public void Hello_ShouldReturnValidResult(string friendName, string expectedResult)
        {
            //Arrange
            var result = HelloSolution.Hello(friendName);

            //Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void Hello_WithNullFriendName_ShouldReturnValidResult()
        {
            //Arrange
            var result = HelloSolution.Hello(null);

            //Assert
            result.Should().Be("Hello, !");
        }
    }
}
