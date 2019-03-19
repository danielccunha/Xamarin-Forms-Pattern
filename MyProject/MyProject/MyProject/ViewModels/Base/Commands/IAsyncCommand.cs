using System.Threading.Tasks;
using System.Windows.Input;

namespace MyProject.ViewModels.Base.Commands
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
