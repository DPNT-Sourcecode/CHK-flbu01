using BeFaster.App.Solutions.TST;
namespace BeFaster.App.Tests.Solutions.TST
{
    public class OneTest {
    
        [Fact]
        public void RunApply() {
            Assert.Equal(One.apply(), 1);
        }
    }
}
