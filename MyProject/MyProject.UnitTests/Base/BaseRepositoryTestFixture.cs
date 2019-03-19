using Moq;
using MyProject.Contracts.Persistence;
using MyProject.Persistence;

namespace MyProject.UnitTests.Base
{
    public abstract class BaseRepositoryTestFixture : BaseTestFixture
    {
        protected IUnitOfWork UnitOfWork { get; set; }

        public BaseRepositoryTestFixture()
        {
            var dbPathProvider = new Mock<IDatabasePathProvider>();
            dbPathProvider.Setup(x => x.GetDatabasePath(It.IsAny<string>())).Returns(@"E:\Projects\Databases\testDatabase.db3");
            UnitOfWork = new UnitOfWork(dbPathProvider.Object);
        }
    }
}
