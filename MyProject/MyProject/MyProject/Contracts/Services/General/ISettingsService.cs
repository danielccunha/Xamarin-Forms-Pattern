namespace MyProject.Contracts.Services.General
{
    public interface ISettingsService
    {
        void Clear();

        bool ContainsKey(string key);

        T Get<T>(string key);

        T Get<T>(string key, T defaultValue = default(T));

        bool Remove(string key);

        void Set(string key, object value);
    }
}
