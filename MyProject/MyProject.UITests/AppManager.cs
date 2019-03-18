using System;
using Xamarin.UITest;

namespace MyProject.UITests
{
    public class AppManager
    {
        const string AppPath = "../../../Binaries/TaskyiOS.app";
        const string IpaBundleId = "com.xamarin.samples.taskytouch";
        private const string AndroidPackageName = "com.companyname.MyProject";

        private static IApp _app;
        public static IApp App
        {
            get
            {
                if (_app == null)
                    throw new NullReferenceException("'AppManager.App' not set. Call 'AppManager.StartApp()' before trying to access it.");

                return _app;
            }
        }

        private static Platform? _platform;
        public static Platform Platform
        {
            get
            {
                if (_platform == null)
                    throw new NullReferenceException("'AppManager.Platform' not set.");

                return _platform.Value;
            }
            set => _platform = value;
        }

        public static void StartApp()
        {
            if (Platform == Platform.Android)
            {
                _app = ConfigureApp
                    .Android                    
                    .InstalledApp(AndroidPackageName)
                    .EnableLocalScreenshots()
                    .DeviceSerial("emulator-5554")
                    .StartApp();
            }

            if (Platform == Platform.iOS)
            {
                _app = ConfigureApp
                    .iOS
                    // Used to run a .app file on an ios simulator:
                    .AppBundle(AppPath)
                    // Used to run a .ipa file on a physical ios device:
                    //.InstalledApp(ipaBundleId)
                    .StartApp();
            }
        }
    }
}
