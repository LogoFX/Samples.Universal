using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

namespace Samples.Universal.Client.Presentation.Shell.Interactivity.Behaviors
{
    public class UpdateSourceOnChangeBehavior : Behavior<DependencyObject>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject is TextBox txt)
            {
                txt.TextChanged += OnTextChanged;
            }
        }

        protected override void OnDetaching()
        {
            if (AssociatedObject is TextBox txt)
            {
                txt.TextChanged -= OnTextChanged;
                return;
            }
            base.OnDetaching();
        }

        static void OnTextChanged(object sender,
          TextChangedEventArgs e)
        {
            if (!(sender is TextBox txt))
                return;
            var be = txt.GetBindingExpression(TextBox.TextProperty);
            be?.UpdateSource();
        }

    }
}
