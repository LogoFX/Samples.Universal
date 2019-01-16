using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

namespace Samples.Universal.Client.Presentation.Shell.Interactivity.Behaviors
{
    public class SelectTextOnFocusBehavior
        : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotFocus += AssociatedObjectGotFocus;
        }

        void AssociatedObjectGotFocus(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject is TextBox box)
                box.SelectAll();
            else if (AssociatedObject is PasswordBox passwordBox)
                passwordBox.SelectAll();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotFocus -= AssociatedObjectGotFocus;
        }
    }
}