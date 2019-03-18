using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MyProject.Contracts.Services.General;

namespace MyProject.UnitTests.Mocks.Services.General
{
    public class MockDialogService : IDialogService
    {
        // TODO: Implement MockDialogService
        public Task<string> DisplayActionSheetAsync(string title, string cancel, string destruction, params string[] buttons)
        {
            throw new NotImplementedException();
        }

        public Task DisplayAlertAsync(string title, string message, string cancel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel)
        {
            throw new NotImplementedException();
        }

        public void ShowToast(string message, TimeSpan? timeSpan = null)
        {
            Debug.WriteLine($"[MockDialogService]: {message}");
        }

        public void ShowToast(ToastConfig toastConfig)
        {
            throw new NotImplementedException();
        }
    }
}
