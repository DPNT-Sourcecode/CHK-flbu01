using BeFaster.App.Solutions.SUM;
using FluentAssertions;

namespace BeFaster.App.Tests.Solutions.SUM
{
    public class SumSolutionTest
    {
        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(0, 0, 0)]
        [InlineData(100, 100, 200)]
        [InlineData(50, 45, 95)]
        public void ComputeSum(int x, int y, int expectedResult)
        {
            //Arrange
            var result = SumSolution.Sum(x, y);

            //Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        [InlineData(101, 1)]
        [InlineData(1, 101)]
        public void ComputeSum_ShouldThrowArgumentOutOfRangeException(int x, int y)
        {
            //Arrange
            var result = () => SumSolution.Sum(x, y);

            //Assert
            result.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
