using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MyProject.Contracts.Persistence;
using MyProject.Contracts.Persistence.Domain;
using MyProject.Persistence;
using MyProject.UnitTests.Base;
using NUnit.Framework;

namespace MyProject.UnitTests.Persistence.Repositories
{
    public class RepositoryTests : BaseTestFixture
    {
        private IUnitOfWork _unitOfWork { get; set; }
        private Mock<IDatabasePathProvider> _dbPathProvider { get; set; }
        private int EntityCount => _unitOfWork.Products.CountAsync().Result;

        public RepositoryTests()
        {
            _dbPathProvider = new Mock<IDatabasePathProvider>();
            _dbPathProvider.Setup(x => x.GetDatabasePath(It.IsAny<string>())).Returns(@"E:\Projects\Databases\testDatabase.db3");
            _unitOfWork = new UnitOfWork(_dbPathProvider.Object);
        }

        public override async Task BeforeEachTestAsync()
        {
            // Using ProductRepository because it's generic
            await _unitOfWork.Products.TruncateAsync();
            await _unitOfWork.Products.AddRangeAsync(EntitiesHelper.FakeProducts);
        }

        [Test]
        public async Task AddAsync_WhenCalled_AddEntityAndReturnPrimaryKey()
        {
            var result = await _unitOfWork.Products.AddAsync(new Product { Name = "Lorem ipsum" });
            
            Assert.That(await _unitOfWork.Products.CountAsync(), Is.EqualTo(21));
            Assert.That(result, Is.EqualTo(21));
        }

        [Test]
        public async Task AddRangeAsync_WhenCalled_AddEntities()
        {
            await _unitOfWork.Products.AddRangeAsync(EntitiesHelper.FakeProducts);

            Assert.That(await _unitOfWork.Products.CountAsync(), Is.EqualTo(EntitiesHelper.FakeProducts.Count() * 2));
        }

        [Test]
        public async Task CountAsync_WhenCalled_ReturnEntitiesCount()
        {
            var result = await _unitOfWork.Products.CountAsync();

            Assert.That(result, Is.EqualTo(EntitiesHelper.FakeProducts.Count()));
        }

        [Test]
        public async Task FindAsync_WhenCalled_FindAllEntitiesAccordingToPredicate()
        {
            var fakeProducts = EntitiesHelper.FakeProducts.Where(p => p.Price > 500);
            var result = await _unitOfWork.Products.FindAsync(p => p.Price > 500);

            Assert.That(result.Count(), Is.EqualTo(fakeProducts.Count()));
            CollectionAssert.AreEquivalent(result, fakeProducts);
        }

        [Test]
        [TestCase(500)]
        [TestCase(1000)]
        public async Task FirstOrDefaultAsync_WhenCalled_FindFirstEntityAccordingToPredicate(double price)
        {
            var fakeProduct = EntitiesHelper.FakeProducts.FirstOrDefault(p => p.Price > price);
            var result = await _unitOfWork.Products.FirstOrDefaultAsync(p => p.Price > price);

            Assert.That(result, Is.EqualTo(fakeProduct));
        }

        [Test]
        public async Task GetAsync_WhenCalled_ReturnEntityByPrimaryKey()
        {
            var fakeProduct = EntitiesHelper.FakeProducts.First();
            fakeProduct.Id = 1;

            var result = await _unitOfWork.Products.GetAsync(1);

            Assert.That(result, Is.EqualTo(fakeProduct));
        }

        [Test]
        public async Task GetAllAsync_WhenCalled_ReturnAllEntities()
        {
            var pk = 1;
            var fakeProducts = EntitiesHelper.FakeProducts.ToList();

            fakeProducts.ForEach(p => p.Id = pk++);

            var result = await _unitOfWork.Products.GetAllAsync();

            CollectionAssert.AreEquivalent(result, fakeProducts);
        }

        [Test]
        public async Task RemoveAsync_EntityHasPk_RemoveEntityFromDatabase()
        {
            var product = await _unitOfWork.Products.GetAsync(1);

            var result = await _unitOfWork.Products.RemoveAsync(product);

            Assert.That(result, Is.True);
            Assert.That(await _unitOfWork.Products.CountAsync(), Is.EqualTo(EntitiesHelper.FakeProducts.Count() - 1));
        }

        [Test]
        public void RemoveAsync_EntityHasNoPk_ThrowNotSupportedException()
        {
            Assert.That(() => _unitOfWork.Products.RemoveAsync(null), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public async Task RemoveRangeAsync_WhenCalled_RemoveEntitiesFromDatabase()
        {
            var products = await _unitOfWork.Products.FindAsync(p => p.Price > 500);

            await _unitOfWork.Products.RemoveRangeAsync(products);

            Assert.That(EntityCount, Is.EqualTo(EntitiesHelper.FakeProducts.Count() - products.Count()));
        }

        [Test]
        public async Task TruncateAsync_WhenCalled_RemoveAllEntitiesFromDatabase()
        {
            await _unitOfWork.Products.TruncateAsync();

            Assert.That(EntityCount, Is.Zero);
        }

        [Test]
        public async Task UpdateAsync_EntityHasPk_UpdateAnEntityAndReturnTrue()
        {
            var product = await _unitOfWork.Products.GetAsync(1);
            product.Name = "Lorem ipsum";

            var result = await _unitOfWork.Products.UpdateAsync(product);
            var updatedProduct = await _unitOfWork.Products.GetAsync(1);

            Assert.That(result, Is.True);
            Assert.That(updatedProduct, Is.EqualTo(product));
        }

        [Test]
        public async Task UpdateAsync_EntityHasNoPk_ReturnFalse()
        {
            var result = await _unitOfWork.Products.UpdateAsync(null);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task UpdateRangeAsync_WhenCalled_UpdateAllEntitiesInARange()
        {
            var products = (await _unitOfWork.Products.FindAsync(p => p.Price > 500)).ToList();
            products.ForEach(p => p.Name += " Updated");

            await _unitOfWork.Products.UpdateRangeAsync(products);

            var updatedProducts = await _unitOfWork.Products.FindAsync(p => p.Price > 500);

            CollectionAssert.AreEquivalent(updatedProducts, products);
        }
    }
}
