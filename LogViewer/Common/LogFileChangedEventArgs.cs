using System;

namespace LogViewer.Core
{
    public class LogFileChangedEventArgs : EventArgs
    {
        public LogFileChangedEventArgs(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}