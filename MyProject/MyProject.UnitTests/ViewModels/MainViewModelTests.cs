using MyProject.UnitTests.Mocks.Persistence;
using MyProject.UnitTests.Mocks.Services;
using MyProject.ViewModels;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.UnitTests.ViewModels
{
    [TestFixture]
    public class MainViewModelTests
    {
        private MainViewModel ViewModel { get; set; }

        [SetUp]
        public async Task SetUp()
        {
            ViewModel = new MainViewModel(new MockDialogService(), new MockNavigationService(), new MockUnitOfWork());
            await ViewModel.InitializeAsync(null);
        }

        [Test]
        public void InitializeAsync_WhenCalled_ShouldInitializeProductsCollection()
        {
            // Test InitializeAsync method called in the set up method
            Assert.That(ViewModel.Products, Is.Not.Null);
            Assert.That(ViewModel.Products.Count, Is.EqualTo(5));
        }

        [Test]
        public void AddCommand_WhenCalled_AddProductToProductsCollection()
        {
            ViewModel.AddCommand.Execute(null);

            Assert.That(ViewModel.Products.Count, Is.EqualTo(6));
            Assert.That(ViewModel.Products.Last().Id, Is.EqualTo(6));
        }

        [Test]
        public void RemoveProductCommand_WhenCalled_RemoveProductFromProductsCollection()
        {
            var product = ViewModel.Products.First();
            ViewModel.RemoveProductCommand.Execute(product);

            Assert.That(ViewModel.Products.Count, Is.EqualTo(4));
            Assert.That(ViewModel.Products.Contains(product), Is.False);
        }
    }
}
