using MyProject.Contracts.Services.General;
using System;
using System.Threading.Tasks;

namespace MyProject.ViewModels.Base
{
    public abstract class ViewModelBase : BindableBase, IDisposable
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;

        protected ViewModelBase(IDialogService dialogService, INavigationService navigationService)
        {
            DialogService = dialogService;
            NavigationService = navigationService;
        }

        public virtual Task InitializeAsync(object parameter)
        {
            return Task.FromResult(false);
        }

        public virtual void Dispose()
        {
            
        }
    }
}
