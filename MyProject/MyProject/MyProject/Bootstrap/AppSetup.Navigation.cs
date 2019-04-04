using MyProject.ViewModels;
using MyProject.Views;
using System;
using System.Collections.Generic;

namespace MyProject.Bootstrap
{
    public partial class AppSetup
    {
        public static void CreatePageViewModelMappings(Dictionary<Type, Type> mappings)
        {
            mappings.Add(typeof(MainViewModel), typeof(MainPage));
        }
    }
}
