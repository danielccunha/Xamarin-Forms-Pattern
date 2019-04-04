using MyProject.ViewModels.Base;
using System;
using System.Threading.Tasks;

namespace MyProject.Contracts.Services.General
{
    public interface INavigationService
    {
        bool IsMasterPagePresented { get; }

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase;

        Task NavigateToAsync(Type viewModelType, object parameter = null);

        Task NavigateToAsync(string pageName, object parameter = null);

        Task NavigateBackAsync();

        Task RemoveLastFromBackStackAsync();

        Task PopToRootAsync();
    }
}
