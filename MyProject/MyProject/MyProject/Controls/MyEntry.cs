using System.Windows.Input;
using Xamarin.Forms;

namespace MyProject.Controls
{
    public class MyEntry : Entry
    {
        /// <summary>
        /// Text Changed Command
        /// </summary>
        public static readonly BindableProperty TextChangedCommandProperty
            = BindableProperty.Create(nameof(TextChangedCommand), typeof(ICommand), typeof(MyEntry));

        public ICommand TextChangedCommand
        {
            get => (ICommand)GetValue(TextChangedCommandProperty);
            set => SetValue(TextChangedCommandProperty, value);
        }

        public MyEntry()
        {
            TextChanged += (s, e) =>
            {
                if (TextChangedCommand != null && TextChangedCommand.CanExecute(null))
                    TextChangedCommand.Execute(e);
            };
        }
    }
}
