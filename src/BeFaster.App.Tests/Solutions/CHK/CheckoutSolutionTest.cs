using BeFaster.App.Solutions.CHK;
using FluentAssertions;

namespace BeFaster.App.Tests.Solutions.CHK
{
    public class CheckoutSolutionTest
    {
        [Theory]
        [InlineData("", 0)]
        [InlineData("A", 50)]
        [InlineData("ACD", 85)]
        [InlineData("ABABC", 165)]
        [InlineData("ABABCE", 205)]
        [InlineData("ABACEE", 200)]
        [InlineData("ABABCEE", 230)]
        [InlineData("ABABCEEA", 260)]
        [InlineData("ABABCEEAAA", 330)]
        [InlineData("ABABCEEAAAA", 380)]
        [InlineData("ABABCEEAAAAAAA", 520)]
        [InlineData("AAAAAAAA", 330)]
        public void ComputePrice_ShouldReturnValidResult(string skus, int expectedResult)
        {
            //Arrange
            var result = CheckoutSolution.ComputePrice(skus);

            //Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("AaCD")]
        [InlineData("ABAxBC")]
        [InlineData("ABA-BC")]
        public void ComputePrice_ShouldReturnInvalidResult(string skus)
        {
            //Arrange
            var result = CheckoutSolution.ComputePrice(skus);

            //Assert
            result.Should().Be(-1);
        }
    }
}




