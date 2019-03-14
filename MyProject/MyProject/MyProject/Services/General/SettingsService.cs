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
        private static IDictionary<string, object> Properties => Application.Current.Properties;

        public void Clear()
        {
            Properties.Clear();
            Save();
        }

        public bool ContainsKey(string key) => Properties.ContainsKey(key);

        public object Get(string key)
        {
            if (!ContainsKey(key))
                throw new ArgumentException($"There is no key called \"{key}\".");

            return Properties[key];
        }

        public bool Remove(string key)
        {
            if (!ContainsKey(key))
                throw new ArgumentException($"There is no key called \"{key}\".");

            if (Properties.Remove(key))
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
                Properties.Remove(key);

            Properties.Add(key, value);
            Save();
        }
    }
}
