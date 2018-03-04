using LogViewer.Core;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LogViewer.Services.Implementation
{
    public class FileWatcher : IFileWatcher
    {
        private bool isStopped;
        private long bytesRead = 0;

        public event LogFileChangedHandler Changed;

        public FileWatcher()
        {
        }

        public async void Start(string filePath)
        {
            isStopped = false;
            //await ReadAsync(filePath);
            await ListenAsync(filePath);
        }

        private async Task ReadAsync(string filePath)
        {
            var totalBytes = new FileInfo(filePath).Length;

            if (bytesRead < totalBytes)
            {
                // bytesRead = await ReadAsync(filePath, bytesRead);
                await Task.Delay(1);
            }
        }

        public void Stop()
        {
            isStopped = true;
        }

        private async Task ListenAsync(string filePath)
        {
            var totalSize = new FileInfo(filePath).Length;
            while (true)
            {
                await Task.Delay(100);
                var currentSize = new FileInfo(filePath).Length;
                var newBytes = currentSize - totalSize;
                if (newBytes > 0)
                {
                    bytesRead = await ReadAsync(filePath, totalSize);
                }
                totalSize = currentSize;

                if (isStopped)
                {
                    break;
                }
            }
        }

        private async Task<long> ReadAsync(string filePath, long offset)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fs.Seek(offset, SeekOrigin.Begin);
                var buffer = new byte[1024];
                string text = "";

                while (true)
                {
                    var bytesRead = fs.Read(buffer, 0, buffer.Length);
                    offset += bytesRead;

                    if (bytesRead == 0)
                        break;

                    text += Encoding.ASCII.GetString(buffer, 0, bytesRead);
                }

                OnChanged(text);
            }

            return offset;
        }

        private void OnChanged(string text) => Changed?.Invoke(this, new LogFileChangedEventArgs(text));
    }
}