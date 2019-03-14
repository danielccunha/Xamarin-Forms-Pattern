using MyProject.Contracts.Services.General;
using MyProject.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyProject.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand PopModalCommand => new Command(PopModal);

        public MainViewModel(IDialogService dialogService, INavigationService navigationService) 
            : base(dialogService, navigationService)
        {

        }        

        private async void PopModal()
        {
            await NavigationService.NavigateToAsync<ModalViewModel>();
            await NavigationService.NavigateToAsync<ModalViewModel>();
            await NavigationService.NavigateToAsync<ModalViewModel>();
            await NavigationService.NavigateToAsync<ModalViewModel>();
        }
    }
}
