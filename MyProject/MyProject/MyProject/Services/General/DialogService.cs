using System;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MyProject.Contracts.Services.General;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MyProject.Services.General
{
    public class DialogService : IDialogService
    {
        protected Application CurrentApplication => Application.Current;

        public Task<string> DisplayActionSheetAsync(string title, string cancel, string destruction, params string[] buttons)
        {
            return GetCurrentPage().DisplayActionSheet(title, cancel, destruction, buttons);
        }

        public Task DisplayAlertAsync(string title, string message, string cancel)
        {
            return GetCurrentPage().DisplayAlert(title, message, cancel);
        }

        public Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel)
        {
            return GetCurrentPage().DisplayAlert(title, message, accept, cancel);
        }

        private Page GetCurrentPage()
        {
            Page page = null;

            if (PopupNavigation.Instance.PopupStack.Count > 0)
                page = PopupNavigation.Instance.PopupStack.LastOrDefault();
            else if (CurrentApplication.MainPage.Navigation.ModalStack.Count > 0)
                page = CurrentApplication.MainPage.Navigation.ModalStack.LastOrDefault();
            else
                page = CurrentApplication.MainPage.Navigation.NavigationStack.LastOrDefault();

            if (page == null)
                page = CurrentApplication.MainPage;

            return page;
        }

        public void ShowToast(string message, TimeSpan? timeSpan = null)
        {
            UserDialogs.Instance.Toast(message, timeSpan);
        }

        public void ShowToast(ToastConfig toastConfig)
        {
            // TODO: Set default ToastConfig style
            UserDialogs.Instance.Toast(toastConfig);
        }
    }
}
