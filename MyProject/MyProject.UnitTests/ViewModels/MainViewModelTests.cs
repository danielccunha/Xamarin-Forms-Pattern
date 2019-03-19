using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MyProject.UnitTests.Base;
using MyProject.ViewModels;
using NUnit.Framework;

namespace MyProject.UnitTests.ViewModels
{
    public class MainViewModelTests : BaseViewModelTestFixture
    {
        private MainViewModel _viewModel { get; set; }

        public override async Task BeforeEachTestAsync()
        {
            _viewModel = new MainViewModel(DialogService.Object, NavigationService.Object, UnitOfWork);
            await _viewModel.InitializeAsync(null);
        }

        [Test]
        public async Task AddProductCommand_WhenCalled_AddProductToProductsCollection()
        {
            var localCount = _viewModel.Products.Count;
            var storageCount = await UnitOfWork.Products.CountAsync();

            await _viewModel.AddProductCommand.ExecuteAsync(null);

            Assert.That(_viewModel.Products.Count, Is.EqualTo(localCount + 1));
            Assert.That(await UnitOfWork.Products.CountAsync(), Is.EqualTo(storageCount + 1));
        }

        [Test]
        public async Task InitializeAsync_WhenCalled_IntializeProductsCollection()
        {
            Assert.That(_viewModel.Products, Is.Not.Null);
            Assert.That(_viewModel.Products.Count, Is.EqualTo(await UnitOfWork.Products.CountAsync()));
        }

        [Test]
        public void RemoveProductCommand_ProductIsNull_ThrowNotSupportedException()
        {
            Assert.That(async () => await _viewModel.RemoveProductCommand.ExecuteAsync(null),
                Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public async Task RemoveProductCommand_ProductHasPk_RemoveProductFromCollections()
        {
            await _viewModel.AddProductCommand.ExecuteAsync(null); // Guarantee it has at least one product

            var product = _viewModel.Products.First();
            var localCount = _viewModel.Products.Count;
            var storageCount = await UnitOfWork.Products.CountAsync();

            await _viewModel.RemoveProductCommand.ExecuteAsync(product);

            DialogService.Verify(x => x.ShowToast(It.IsAny<string>(), null));
            Assert.That(_viewModel.Products.Count, Is.EqualTo(localCount - 1));
            Assert.That(await UnitOfWork.Products.CountAsync(), Is.EqualTo(storageCount - 1));
        }
    }
}
