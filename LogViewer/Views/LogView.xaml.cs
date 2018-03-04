using LogViewer.Core;
using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace LogViewer.Views
{
    /// <summary>
    /// Interaction logic for LogView.xaml
    /// </summary>
    public partial class LogView : UserControl
    {
        private TokenHighlighter tokenHighlighter;

        public LogView()
        {
            InitializeComponent();
            tokenHighlighter = new TokenHighlighter(richTextBox);
        }

        private async void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            scrollViewer.ScrollToEnd();

            await Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() =>
            {
                tokenHighlighter.Highlight(e.Changes);
            }));
        }
    }
}