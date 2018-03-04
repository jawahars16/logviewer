using LogViewer.Core;

namespace LogViewer.Services
{
    public interface IFileWatcher
    {
        void Start(string filePath);

        void Stop();

        event LogFileChangedHandler Changed;
    }
}