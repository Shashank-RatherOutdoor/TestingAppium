using NUnit.Framework;

namespace TestingAppium.Tests
{
    [TestFixture]
    public class SimpleTests
    {
        [Test]
        public void SimpleTest()
        {
            Assert.Pass("This test passed successfully.");
        }
    }
}