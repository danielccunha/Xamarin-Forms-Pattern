using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MyProject.Contracts.Persistence;
using MyProject.Contracts.Persistence.Domain;
using MyProject.Contracts.Services.General;
using MyProject.Extensions;
using MyProject.ViewModels.Base;
using MyProject.ViewModels.Base.Commands;

namespace MyProject.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products { get => _products; set => SetProperty(ref _products, value); }

        public IAsyncCommand AddProductCommand => new AsyncCommand(AddProduct);
        public IAsyncCommand RemoveProductCommand => new AsyncCommand<Product>(RemoveProduct);

        public MainViewModel(IDialogService dialogService, INavigationService navigationService, IUnitOfWork unitOfWork)
            : base(dialogService, navigationService, unitOfWork)
        {
            
        }

        public override async Task InitializeAsync(object parameter)
        {
            Products = await UnitOfWork.Products.GetAllAsync().ToObservableCollectionAsync();
        }

        private async Task AddProduct()
        {
            var product = new Product
            {
                Name = $"Product {DateTime.Now.Ticks}",
                Price = new Random().Next(1, 1000)
            };

            product.Id = await UnitOfWork.Products.AddAsync(product);

            Products.Add(product);
        }

        private async Task RemoveProduct(Product product)
        {
            if (await UnitOfWork.Products.RemoveAsync(product))
            {
                Products.Remove(product);
                DialogService.ShowToast("Product removed successfully.");
            }
        }
    }
}
