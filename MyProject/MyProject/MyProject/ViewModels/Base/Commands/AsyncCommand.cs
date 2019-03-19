using System;
using System.Threading.Tasks;

namespace MyProject.ViewModels.Base.Commands
{
    public class AsyncCommand : IAsyncCommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Func<object, Task> _execute;
        private readonly Func<object, bool> _canExecute;

        public AsyncCommand(Func<Task> execute)
        {
            if (execute == null)
                ThrowArgumentNullException(nameof(execute));

            _execute = new Func<object, Task>(obj => execute());
        }

        public AsyncCommand(Func<object, Task> execute)
        {
            if (execute == null)
                ThrowArgumentNullException(nameof(execute));

            _execute = execute;
        }

        public AsyncCommand(Func<Task> execute, Func<bool> canExecute)
        {
            if (execute == null)
                ThrowArgumentNullException(nameof(execute));

            if (canExecute == null)
                ThrowArgumentNullException(nameof(canExecute));

            _execute = new Func<object, Task>(obj => execute());
            _canExecute = new Func<object, bool>(obj => canExecute());
        }

        public AsyncCommand(Func<object, Task> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
                ThrowArgumentNullException(nameof(execute));

            if (canExecute == null)
                ThrowArgumentNullException(nameof(canExecute));

            _execute = execute;
            _canExecute = canExecute;
        }

        public async Task ExecuteAsync(object parameter)
        {
            if (CanExecute(parameter))
                await _execute(parameter);
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        private void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ThrowArgumentNullException(string actionName)
        {
            throw new ArgumentNullException($"Action must not be null. Action name: {actionName}.");
        }
    }

    public class AsyncCommand<T> : AsyncCommand
    {
        public AsyncCommand(Func<T, Task> execute)
            : base(obj => execute((T)obj))
        {

        }

        public AsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute)
            : base(obj => execute((T)obj), obj => canExecute((T)obj))
        {

        }
    }
}
