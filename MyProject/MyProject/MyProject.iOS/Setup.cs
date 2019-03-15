using Autofac;
using MyProject.Bootstrap;
using MyProject.Contracts.Persistence;
using MyProject.iOS.Persistence;

namespace MyProject.iOS
{
    public class Setup : AppSetup
    {
        protected override void RegisterDependencies(ContainerBuilder builder)
        {
            base.RegisterDependencies(builder);

            // Persistence
            builder.RegisterType<DatabasePathProvider>().As<IDatabasePathProvider>();
        }
    }
}