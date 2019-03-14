using MyProject.Contracts.Services.General;
using MyProject.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyProject.ViewModels
{
    public class ModalViewModel : ViewModelBase
    {
        public ICommand BoxTappedCommand => new Command(BoxTapped);

        public ModalViewModel(IDialogService dialogService, INavigationService navigationService) 
            : base(dialogService, navigationService)
        {
        }

        private async void BoxTapped()
        {
            await DialogService.DisplayAlertAsync("Title", "Message", "Ok");
            await NavigationService.NavigateBackAsync();
        }
    }
}
