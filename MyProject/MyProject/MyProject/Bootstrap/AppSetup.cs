using Autofac;
using MyProject.Contracts.Persistence;
using MyProject.Contracts.Services.Data;
using MyProject.Contracts.Services.General;
using MyProject.Persistence;
using MyProject.Services.Data;
using MyProject.Services.General;
using MyProject.ViewModels;

namespace MyProject.Bootstrap
{
    public partial class AppSetup
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

            // Services - Data
            builder.RegisterType<RequestProvider>().As<IRequestProvider>();

            // Persistence
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        }
    }
}
