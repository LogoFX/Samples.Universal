using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace Samples.Universal.Client.Presentation.Shell.DependencyProperties
{
    public static class TitleDependencyProperty
    {
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.RegisterAttached("Title", typeof(string),
                typeof(TitleDependencyProperty),
                new PropertyMetadata(string.Empty, OnTitlePropertyChanged));

        public static string GetTitle(DependencyObject d)
        {
            return d.GetValue(TitleProperty).ToString();
        }

        public static void SetTitle(DependencyObject d, string value)
        {
            d.SetValue(TitleProperty, value);
        }

        private static void OnTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var title = e.NewValue.ToString();
            var window = ApplicationView.GetForCurrentView();
            window.Title = title;
        }
    }
}
