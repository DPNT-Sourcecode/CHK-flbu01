using BeFaster.App.Solutions.SUM;

namespace BeFaster.App.Tests.Solutions.SUM
{
    public class SumSolutionTest
    {
        [Theory]
        [InlineData(1, 1, 2)]
        public void ComputeSum(int x, int y, int expectedResult)
        {
            Assert.Equal(SumSolution.Sum(x, y), expectedResult);
        }
    }
}
