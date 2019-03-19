using System;
using System.Linq;
using System.Threading.Tasks;
using MyProject.Contracts.Persistence.Domain;
using MyProject.UnitTests.Base;
using NUnit.Framework;

namespace MyProject.UnitTests.Persistence.Repositories
{
    public class RepositoryTests : BaseRepositoryTestFixture
    {
        private int EntityCount => UnitOfWork.Products.CountAsync().Result;

        public RepositoryTests() : base() { }

        public override async Task BeforeEachTestAsync()
        {
            // Using ProductRepository because it's generic
            await UnitOfWork.Products.TruncateAsync();
            await UnitOfWork.Products.AddRangeAsync(EntitiesHelper.FakeProducts);
        }

        [Test]
        public async Task AddAsync_WhenCalled_AddEntityAndReturnPrimaryKey()
        {
            var result = await UnitOfWork.Products.AddAsync(new Product { Name = "Lorem ipsum" });
            
            Assert.That(await UnitOfWork.Products.CountAsync(), Is.EqualTo(21));
            Assert.That(result, Is.EqualTo(21));
        }

        [Test]
        public async Task AddRangeAsync_WhenCalled_AddEntities()
        {
            await UnitOfWork.Products.AddRangeAsync(EntitiesHelper.FakeProducts);

            Assert.That(await UnitOfWork.Products.CountAsync(), Is.EqualTo(EntitiesHelper.FakeProducts.Count() * 2));
        }

        [Test]
        public async Task CountAsync_WhenCalled_ReturnEntitiesCount()
        {
            var result = await UnitOfWork.Products.CountAsync();

            Assert.That(result, Is.EqualTo(EntitiesHelper.FakeProducts.Count()));
        }

        [Test]
        public async Task FindAsync_WhenCalled_FindAllEntitiesAccordingToPredicate()
        {
            var fakeProducts = EntitiesHelper.FakeProducts.Where(p => p.Price > 500);
            var result = await UnitOfWork.Products.FindAsync(p => p.Price > 500);

            Assert.That(result.Count(), Is.EqualTo(fakeProducts.Count()));
            CollectionAssert.AreEquivalent(result, fakeProducts);
        }

        [Test]
        [TestCase(500)]
        [TestCase(1000)]
        public async Task FirstOrDefaultAsync_WhenCalled_FindFirstEntityAccordingToPredicate(double price)
        {
            var fakeProduct = EntitiesHelper.FakeProducts.FirstOrDefault(p => p.Price > price);
            var result = await UnitOfWork.Products.FirstOrDefaultAsync(p => p.Price > price);

            Assert.That(result, Is.EqualTo(fakeProduct));
        }

        [Test]
        public async Task GetAsync_WhenCalled_ReturnEntityByPrimaryKey()
        {
            var fakeProduct = EntitiesHelper.FakeProducts.First();
            fakeProduct.Id = 1;

            var result = await UnitOfWork.Products.GetAsync(1);

            Assert.That(result, Is.EqualTo(fakeProduct));
        }

        [Test]
        public async Task GetAllAsync_WhenCalled_ReturnAllEntities()
        {
            var pk = 1;
            var fakeProducts = EntitiesHelper.FakeProducts.ToList();

            fakeProducts.ForEach(p => p.Id = pk++);

            var result = await UnitOfWork.Products.GetAllAsync();

            CollectionAssert.AreEquivalent(result, fakeProducts);
        }

        [Test]
        public async Task RemoveAsync_EntityHasPk_RemoveEntityFromDatabase()
        {
            var product = await UnitOfWork.Products.GetAsync(1);

            var result = await UnitOfWork.Products.RemoveAsync(product);

            Assert.That(result, Is.True);
            Assert.That(await UnitOfWork.Products.CountAsync(), Is.EqualTo(EntitiesHelper.FakeProducts.Count() - 1));
        }

        [Test]
        public void RemoveAsync_EntityHasNoPk_ThrowNotSupportedException()
        {
            Assert.That(() => UnitOfWork.Products.RemoveAsync(null), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public async Task RemoveRangeAsync_WhenCalled_RemoveEntitiesFromDatabase()
        {
            var products = await UnitOfWork.Products.FindAsync(p => p.Price > 500);

            await UnitOfWork.Products.RemoveRangeAsync(products);

            Assert.That(EntityCount, Is.EqualTo(EntitiesHelper.FakeProducts.Count() - products.Count()));
        }

        [Test]
        public async Task TruncateAsync_WhenCalled_RemoveAllEntitiesFromDatabase()
        {
            await UnitOfWork.Products.TruncateAsync();

            Assert.That(EntityCount, Is.Zero);
        }

        [Test]
        public async Task UpdateAsync_EntityHasPk_UpdateAnEntityAndReturnTrue()
        {
            var product = await UnitOfWork.Products.GetAsync(1);
            product.Name = "Lorem ipsum";

            var result = await UnitOfWork.Products.UpdateAsync(product);
            var updatedProduct = await UnitOfWork.Products.GetAsync(1);

            Assert.That(result, Is.True);
            Assert.That(updatedProduct, Is.EqualTo(product));
        }

        [Test]
        public async Task UpdateAsync_EntityHasNoPk_ReturnFalse()
        {
            var result = await UnitOfWork.Products.UpdateAsync(null);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task UpdateRangeAsync_WhenCalled_UpdateAllEntitiesInARange()
        {
            var products = (await UnitOfWork.Products.FindAsync(p => p.Price > 500)).ToList();
            products.ForEach(p => p.Name += " Updated");

            await UnitOfWork.Products.UpdateRangeAsync(products);

            var updatedProducts = await UnitOfWork.Products.FindAsync(p => p.Price > 500);

            CollectionAssert.AreEquivalent(updatedProducts, products);
        }
    }
}
