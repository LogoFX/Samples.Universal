using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.Xaml.Interactivity;

namespace Samples.Universal.Client.Presentation.Shell.Interactivity.Behaviors
{
    public class DataGridSelectionBehavior : Behavior<DataGrid>
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(DataGridSelectionBehavior),
                new PropertyMetadata(null));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;

            base.OnDetaching();
        }

        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Command == null)
            {
                return;
            }

            if (Command.CanExecute(e))
            {
                Command.Execute(e);
            }
        }
    }
}