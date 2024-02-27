using BeFaster.App.Solutions.HLO;
using BeFaster.App.Solutions.SUM;
using FluentAssertions;

namespace BeFaster.App.Tests.Solutions.SUM
{
    public class HelloSolutionTest
    {
        [Theory]
        [InlineData("John", "Hello, John!")]
        [InlineData("", "Hello, !")]
        public void Hello(string friendName, string expectedResult)
        {
            //Arrange
            var result = HelloSolution.Hello(friendName);

            //Assert
            result.Should().Be(expectedResult);
        }
    }
}


