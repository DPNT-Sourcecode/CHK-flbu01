using BeFaster.App.Solutions.TST;
namespace BeFaster.App.Tests.Solutions.TST
{
    public class OneTest {
    
        [Fact]
        public void RunApply() {
            Assert.Equal(1, One.apply());
        }
    }
}
