using MyProject.Bootstrap;
using MyProject.Contracts.Services.General;

namespace MyProject.Utility
{
    public static class AppSettings
    {
        private static ISettingsService Settings { get; } = AppContainer.Resolve<ISettingsService>();

        // TODO: Add application properties
    }
}
