using System.Windows;

namespace LogViewer.Core
{
    public class TokenStyle
    {
        public TokenStyle(DependencyProperty property, object value)
        {
            Property = property;
            Value = value;
        }

        public DependencyProperty Property { get; set; }

        public object Value { get; set; }
    }
}