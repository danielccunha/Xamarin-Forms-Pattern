using MyProject.Contracts.Services.General;
using MyProject.ViewModels.Base;

namespace MyProject.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        public AboutViewModel(IDialogService dialogService, INavigationService navigationService) 
            : base(dialogService, navigationService)
        {
        }
    }
}
