using BeFaster.App.Solutions.TST;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace BeFaster.App.Tests.Solutions.TST
{
    public class OneTest {
    
        [Test]
        public void RunApply() {
            ClassicAssert.AreEqual(One.apply(), 1);
        }
    }
}

