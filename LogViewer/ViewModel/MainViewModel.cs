using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LogViewer.Services;
using System;
using System.Collections.ObjectModel;

namespace LogViewer.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IFileOpenService fileOpenService;
        private LogViewModel activeSession;
        private LogViewModel lastLogSession;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IFileOpenService fileOpenService)
        {
            this.fileOpenService = fileOpenService;

            OpenFileCommand = new RelayCommand(OnOpenFile);
            LogSessions = new ObservableCollection<LogViewModel>();
        }

        public LogViewModel ActiveSession
        {
            get { return activeSession; }
            set
            {
                if (activeSession != null)
                {
                    activeSession.Stop();
                }
                activeSession = value;
                RaisePropertyChanged();
                activeSession.Start();
            }
        }

        public ObservableCollection<LogViewModel> LogSessions { get; set; }

        public RelayCommand OpenFileCommand { get; private set; }

        private void OnOpenFile()
        {
            string filePath = fileOpenService.OpenFile();

            if (filePath != null)
            {
                var fileWatcher = ServiceLocator.Current.GetInstance<IFileWatcher>(DateTime.Now.Ticks.ToString());
                var logSession = new LogViewModel(fileWatcher, filePath);
                LogSessions.Add(logSession);
                ActiveSession = logSession;
            }
        }
    }
}