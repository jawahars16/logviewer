using GalaSoft.MvvmLight;
using LogViewer.Core;
using LogViewer.Services;
using System.IO;

namespace LogViewer.ViewModel
{
    public class LogViewModel : ViewModelBase
    {
        private readonly IFileWatcher fileWatcher;
        private string filePath;
        private string latestLog;
        private string title;

        public LogViewModel(IFileWatcher fileWatcher, string filePath)
        {
            this.fileWatcher = fileWatcher;
            this.filePath = filePath;

            Title = Path.GetFileName(filePath);
        }

        public string LatestLog
        {
            get { return latestLog; }
            set { latestLog = value; RaisePropertyChanged(); }
        }

        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }

        public void Start()
        {
            fileWatcher.Start(filePath);
            fileWatcher.Changed += OnChanged;
        }

        public void Stop()
        {
            fileWatcher.Stop();
            fileWatcher.Changed -= OnChanged;
        }

        private void OnChanged(object sender, LogFileChangedEventArgs e)
        {
            LatestLog = e.Text;
        }
    }
}