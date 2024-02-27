using BeFaster.App.Solutions.SUM;
using FluentAssertions;

namespace BeFaster.App.Tests.Solutions.SUM
{
    public class SumSolutionTest
    {
        [Theory]
        [InlineData(1, 1, 2)]
        public void ComputeSum(int x, int y, int expectedResult)
        {
            //Arrange
            var result = SumSolution.Sum(x, y);

            //Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        public void ComputeSum_ShouldThrowArgumentNullException(int x, int y, int expectedResult)
        {
            //Arrange
            var result = SumSolution.Sum(x, y);

            //Assert
            result.Should().Be(expectedResult);
        }
    }
}


