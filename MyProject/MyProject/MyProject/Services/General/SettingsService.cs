using MyProject.Contracts.Services.General;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyProject.Services.General
{
    public class SettingsService : ISettingsService
    {
        private static IDictionary<string, object> Settings => Application.Current.Properties;

        public void Clear()
        {
            Settings.Clear();
            Save();
        }

        public bool ContainsKey(string key) => Settings.ContainsKey(key);

        public T Get<T>(string key)
        {
            if (!ContainsKey(key))
                return default(T);

            return (T)Settings[key];
        }

        public T Get<T>(string key, T defaultValue = default(T))
        {
            if (!ContainsKey(key))
                return defaultValue;

            return (T)Settings[key];
        }

        public bool Remove(string key)
        {
            if (!ContainsKey(key))
                throw new ArgumentException($"There is no key called \"{key}\".");

            if (Settings.Remove(key))
            {
                Save();
                return true;
            }

            return false;
        }

        private async void Save()
        {
            try
            {
                await Application.Current.SavePropertiesAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Failed to save application properties: {e.Message}");
            }
        }

        public void Set(string key, object value)
        {
            if (ContainsKey(key))
                Settings.Remove(key);

            Settings.Add(key, value);
            Save();
        }
    }
}
