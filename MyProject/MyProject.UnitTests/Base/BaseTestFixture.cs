using NUnit.Framework;
using System.Threading.Tasks;

namespace MyProject.UnitTests.Base
{
    [TestFixture]
    public abstract class BaseTestFixture
    {
        [SetUp]
        public virtual void BeforeEachTest()
        {

        }

        [SetUp]
        public virtual Task BeforeEachTestAsync()
        {
            return Task.CompletedTask;
        }

        [TearDown]
        public virtual void AfterEachTest()
        {

        }

        [TearDown]
        public virtual Task AfterEachTestAsync()
        {
            return Task.CompletedTask;
        }
    }
}
