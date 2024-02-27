using BeFaster.App.Solutions.TST;
using FluentAssertions;
namespace BeFaster.App.Tests.Solutions.TST
{
    public class OneTest
    {

        [Fact]
        public void RunApply()
        {
            //Arrange
            var result = One.apply();

            //Assert
            result.Should().Be(1);
        }
    }
}
