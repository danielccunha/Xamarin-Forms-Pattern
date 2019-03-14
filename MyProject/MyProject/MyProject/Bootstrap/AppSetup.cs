using Autofac;
using MyProject.Contracts.Services.General;
using MyProject.Services.General;
using MyProject.ViewModels;

namespace MyProject.Bootstrap
{
    public class AppSetup
    {
        public IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            RegisterDependencies(builder);

            return builder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder builder)
        {
            // ViewModels
            builder.RegisterType<MainViewModel>().SingleInstance();

            // Services - General
            builder.RegisterType<DependencyService>().As<IDependencyService>();
            builder.RegisterType<DialogService>().As<IDialogService>();
            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<SettingsService>().As<ISettingsService>();
        }
    }
}
