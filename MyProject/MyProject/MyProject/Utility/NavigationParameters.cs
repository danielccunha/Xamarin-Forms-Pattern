using System.Collections.Generic;

namespace MyProject.Utility
{
    public class NavigationParameters : Dictionary<string, object>
    {
        public T Get<T>(string key, T defaultValue = default)
        {
            if (!ContainsKey(key))
                return defaultValue;

            return (T)this[key];
        }
    }
}
