using Autofac;
using MyProject.Bootstrap;

namespace MyProject.iOS
{
    public class Setup : AppSetup
    {
        protected override void RegisterDependencies(ContainerBuilder builder)
        {
            base.RegisterDependencies(builder);

            // TODO: Register services
        }
    }
}