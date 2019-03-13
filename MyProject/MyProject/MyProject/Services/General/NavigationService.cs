using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyProject.Bootstrap;
using MyProject.Contracts.Services.General;
using MyProject.ViewModels;
using MyProject.ViewModels.Base;
using MyProject.Views;
using Xamarin.Forms;

namespace MyProject.Services.General
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication => Application.Current;

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();
            CreatePageViewModelMappings();
        }

        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(MainViewModel), typeof(MainPage));
        }

        public Task ClearBackStack()
        {
            throw new NotImplementedException();
        }

        public Task InitializeAsync()
        {
            return NavigateToAsync<MainViewModel>();
        }

        public Task NavigateBackAsync()
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            throw new NotImplementedException();
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            // TODO: Implement NavigationService as in the base project
            Page page = CreateAndBindPage(viewModelType);

            if (page is MainPage)
            {
                CurrentApplication.MainPage = new NavigationPage(page);
            }
            else if (CurrentApplication.MainPage is MainPage)
            {
                await (CurrentApplication.MainPage as NavigationPage).PushAsync(page);
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        private Page CreateAndBindPage(Type viewModelType)
        {
            // TODO: Change it to receive parameters
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
                throw new Exception($"Mapping type for {viewModelType} is not a page");

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = AppContainer.Resolve(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;

            return page;
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");            

            return _mappings[viewModelType];
        }

        public Task PopToRootAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveLastFromBackStackAsync()
        {
            throw new NotImplementedException();
        }
    }
}
