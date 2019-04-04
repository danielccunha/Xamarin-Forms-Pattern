using MyProject.Contracts.Persistence;
using MyProject.Contracts.Services.General;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyProject.ViewModels.Base
{
    public abstract class ViewModelBase : BindableBase, IDisposable
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;
        protected readonly IUnitOfWork UnitOfWork;

        protected bool IsBusy { get; set; }

        public ICommand AppearingCommand => new Command(Appearing);
        public ICommand DisappearingCommand => new Command(Disappearing);

        protected ViewModelBase(
            IDialogService dialogService, 
            INavigationService navigationService, 
            IUnitOfWork unitOfWork)
        {
            DialogService = dialogService;
            NavigationService = navigationService;
            UnitOfWork = unitOfWork;
        }

        protected virtual void Appearing()
        {

        }

        protected virtual void Disappearing()
        {

        }

        protected virtual void SubscribeEvents()
        {

        }

        protected virtual void UnsubscribeEvents()
        {

        }

        public virtual async Task InitializeAsync(object parameter)
        {
            await UnitOfWork.InitializeAsync();

            SubscribeEvents();
        }

        public virtual void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}
