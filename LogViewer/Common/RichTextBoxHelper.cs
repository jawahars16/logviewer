using System.Windows;
using System.Windows.Controls;

namespace LogViewer.Core
{
    public class RichTextBoxHelper
    {
        public static string GetLatestLog(DependencyObject obj)
        {
            return (string)obj.GetValue(LatestLogProperty);
        }

        public static void SetLatestLog(DependencyObject obj, string value)
        {
            obj.SetValue(LatestLogProperty, value);
        }

        // Using a DependencyProperty as the backing store for LatestLog.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LatestLogProperty =
            DependencyProperty.RegisterAttached("LatestLog", typeof(string), typeof(RichTextBoxHelper), new PropertyMetadata(string.Empty, OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as RichTextBox;

            if (control != null)
            {
                control.AppendText(e.NewValue?.ToString());

                if (e.OldValue != null && e.NewValue is null)
                {
                    control.Document.Blocks.Clear();
                }
            }
        }
    }
}