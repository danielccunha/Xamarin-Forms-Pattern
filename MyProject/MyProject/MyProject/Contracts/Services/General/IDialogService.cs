using Acr.UserDialogs;
using System;
using System.Threading.Tasks;

namespace MyProject.Contracts.Services.General
{
    public interface IDialogService
    {
        Task<string> DisplayActionSheetAsync(string title, string cancel, string destruction, params string[] buttons);

        Task DisplayAlertAsync(string title, string message, string cancel);

        Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel);

        void ShowToast(string message, TimeSpan? timeSpan = null);

        void ShowToast(ToastConfig toastConfig);
    }
}
