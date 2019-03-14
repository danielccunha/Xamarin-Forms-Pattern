using System.Threading.Tasks;

namespace MyProject.Contracts.Services.General
{
    public interface ISettingsService
    {
        void Clear();
        bool ContainsKey(string key);
        object Get(string key);
        bool Remove(string key);
        void Set(string key, object value);
    }
}
