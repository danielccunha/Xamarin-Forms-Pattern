using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MyProject.Contracts.Persistence;
using MyProject.Contracts.Persistence.Domain;
using MyProject.Contracts.Services.General;
using MyProject.Extensions;
using MyProject.ViewModels.Base;
using Xamarin.Forms;

namespace MyProject.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products { get => _products; set => SetProperty(ref _products, value); }

        public ICommand AddCommand => new Command(AddProduct);
        public ICommand RemoveProductCommand => new Command<Product>(RemoveProduct);

        public MainViewModel(IDialogService dialogService, INavigationService navigationService, IUnitOfWork unitOfWork)
            : base(dialogService, navigationService)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task InitializeAsync(object parameter)
        {
            Products = await _unitOfWork.Products.GetAllAsync().ToObservableCollectionAsync();
        }

        private async void AddProduct()
        {
            var product = new Product
            {
                Name = $"Product {DateTime.Now.Ticks}",
                Price = new Random().Next(1, 1000)
            };

            product.Id = await _unitOfWork.Products.AddAsync(product);

            Products.Add(product);
        }

        private async void RemoveProduct(Product product)
        {
            if (await _unitOfWork.Products.RemoveAsync(product))
            {
                Products.Remove(product);
                DialogService.ShowToast("Product removed successfully.");
            }
        }
    }
}
