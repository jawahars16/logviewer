using Microsoft.Win32;

namespace LogViewer.Services.Implementation
{
    public class FileOpenService : IFileOpenService
    {
        public string OpenFile()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            bool? result = dialog.ShowDialog();
            string fileName = null;

            if (result.HasValue)
            {
                fileName = dialog.FileName;
            }

            return fileName;
        }
    }
}