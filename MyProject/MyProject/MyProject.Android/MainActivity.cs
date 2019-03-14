using Android.App;
using Android.Content.PM;
using Android.OS;

namespace MyProject.Droid
{
    [Activity(Label = "MyProject", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            InitializeLibraries(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.Forms.FormsMaterial.Init(this, bundle);

            LoadApplication(new App(new Setup()));
        }

        private void InitializeLibraries(Bundle bundle)
        {
            Rg.Plugins.Popup.Popup.Init(this, bundle);
        }
    }
}