using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyProject.Bootstrap;
using MyProject.Contracts.Services.General;
using MyProject.ViewModels;
using MyProject.ViewModels.Base;
using MyProject.Views;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace MyProject.Services.General
{
    public class NavigationService : INavigationService
    {
        /// <summary>
        /// Dictionary used for mapping ViewModels and its Views
        /// </summary>
        private readonly Dictionary<Type, Type> _mappings;

        /// <summary>
        /// Navigation stack of the MainPage
        /// </summary>
        private INavigation Navigation => (CurrentApplication.MainPage.GetType() == typeof(NavigationPage))
            ? ((NavigationPage)Application.Current.MainPage).Navigation
            : ((NavigationPage)((MasterDetailPage)Application.Current.MainPage).Detail).Navigation;

        /// <summary>
        /// Popup Navigation stack
        /// </summary>
        private IPopupNavigation PopupNavigation => Rg.Plugins.Popup.Services.PopupNavigation.Instance;

        protected Application CurrentApplication => Application.Current;

        public bool IsMasterPagePresented => CurrentApplication.MainPage is MasterDetailPage;

        public NavigationService()
        {
            _mappings = new Dictionary<Type, Type>();
            AppSetup.CreatePageViewModelMappings(_mappings);
        }

        public Task InitializeAsync()
        {
            return NavigateToAsync<MainViewModel>();
        }

        public async Task NavigateBackAsync()
        {
            Page page;
            if (PopupNavigation.PopupStack.Count > 0)
            {
                page = PopupNavigation.PopupStack.Last();
                await PopupNavigation.PopAsync();
            }
            else
                page = await Navigation.PopAsync();

            (page.BindingContext as ViewModelBase).Dispose();
        }

        public Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter = null)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        public async Task NavigateToAsync(string pageName, object parameter = null)
        {
            var pair = _mappings.FirstOrDefault(p => p.Value.Name.Equals(pageName));

            if (pair.Key != null)
                await InternalNavigateToAsync(pair.Key, parameter);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType);
            bool isPopupPage = page is PopupPage;

            // It's not allowed to push a ContentPage after a PopupPage
            if (!isPopupPage && PopupNavigation.PopupStack.Count > 0)
            {
                (page.BindingContext as ViewModelBase).Dispose();
                return;
            }

            if (page is MainPage)
            {
                CurrentApplication.MainPage = new NavigationPage(page);
            }
            else if (isPopupPage)
            {
                await PopupNavigation.PushAsync(page as PopupPage);
            }
            else
            {
                await Navigation.PushAsync(page);
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        private Page CreateAndBindPage(Type viewModelType)
        {
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

        public async Task PopToRootAsync()
        {
            if (PopupNavigation.PopupStack.Count > 0)
            {
                for (int index = 1; index < PopupNavigation.PopupStack.Count; index++)
                    (PopupNavigation.PopupStack[index].BindingContext as ViewModelBase).Dispose();

                await PopupNavigation.PopAllAsync();
            }

            for (int index = 1; index < Navigation.NavigationStack.Count; index++)
                (Navigation.NavigationStack[index].BindingContext as ViewModelBase).Dispose();

            await Navigation.PopToRootAsync();
        }

        public async Task RemoveLastFromBackStackAsync()
        {
            int popupCount = PopupNavigation.PopupStack.Count;
            Page page = null;

            if (popupCount >= 2)
            {
                page = PopupNavigation.PopupStack[popupCount - 2];
                await PopupNavigation.RemovePageAsync(page as PopupPage);
            }
            else if (popupCount == 0 && Navigation.NavigationStack.Count >= 2)
            {
                page = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                Navigation.RemovePage(page);
            }

            if (page != null)
                (page.BindingContext as ViewModelBase).Dispose();
        }
    }
}
