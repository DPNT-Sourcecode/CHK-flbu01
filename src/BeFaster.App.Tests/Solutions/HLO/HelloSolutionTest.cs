using BeFaster.App.Solutions.HLO;
using BeFaster.App.Solutions.SUM;
using FluentAssertions;

namespace BeFaster.App.Tests.Solutions.SUM
{
    public class HelloSolutionTest
    {
        [Fact]
        public void Hello()
        {
            //Arrange
            var result = HelloSolution.Hello("string");

            //Assert
            result.Should().Be("Hello, World!");
        }
    }
}

