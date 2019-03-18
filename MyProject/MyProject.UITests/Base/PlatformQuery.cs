using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace MyProject.UITests.Base
{
    public class PlatformQuery
    {
        public Func<AppQuery, AppQuery> Android
        {
            set
            {
                if (AppManager.Platform == Platform.Android)
                    current = value;
            }
        }

#pragma warning disable IDE1006 // Naming Styles
        public Func<AppQuery, AppQuery> iOS
#pragma warning restore IDE1006 // Naming Styles
        {
            set
            {
                if (AppManager.Platform == Platform.iOS)
                    current = value;
            }
        }

        Func<AppQuery, AppQuery> current;
        public Func<AppQuery, AppQuery> Current
        {
            get
            {
                if (current == null)
                    throw new NullReferenceException("Trait not set for current platform");

                return current;
            }
        }
    }
}
