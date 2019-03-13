using System.Threading.Tasks;

namespace MyProject.ViewModels.Base
{
    public abstract class ViewModelBase : BindableBase
    {
        public virtual Task InitializeAsync(object parameter)
        {
            return Task.FromResult(false);
        }
    }
}
