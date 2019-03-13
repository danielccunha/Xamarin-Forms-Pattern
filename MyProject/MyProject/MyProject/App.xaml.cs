using MyProject.Bootstrap;
using MyProject.Contracts.Services.General;
using MyProject.Views;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyProject
{
    public partial class App : Application
    {
        public App(AppSetup setup)
        {
            InitializeComponent();

            AppContainer.Container = setup.CreateContainer();

            InitializeNavigation();
        }

        private async void InitializeNavigation()
        {
            var navigationService = AppContainer.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }
    }
}
