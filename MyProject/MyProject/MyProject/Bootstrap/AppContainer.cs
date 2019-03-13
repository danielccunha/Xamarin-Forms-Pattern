using Autofac;
using System;

namespace MyProject.Bootstrap
{
    public class AppContainer
    {
        public static IContainer Container { get; set; }

        public static object Resolve(Type typeName)
        {
            return Container.Resolve(typeName);
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
