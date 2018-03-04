using LogViewer.Services;
using LogViewer.ViewModel;
using Moq;
using NUnit.Framework;

namespace LogViewer.Test
{
    [TestFixture]
    public class MainViewModelTest
    {
        [TestCase]
        public void OpenFile_ReturnFileName_ShouldStartWatch()
        {
            string filePath = "sample_log_file";

            var fileOpenService = new Mock<IFileOpenService>();
            fileOpenService.Setup(t => t.OpenFile()).Returns(filePath);

            var fileWatcher = new Mock<IFileWatcher>();

            var viewModel = new MainViewModel(fileOpenService.Object, fileWatcher.Object);
            viewModel.OpenFileCommand.Execute(null);
            fileWatcher.Verify(t => t.Start(filePath), Times.Once);
        }

        [TestCase]
        public void OpenFile_ReturnNull_ShouldNotStartWatch()
        {
            string filePath = null;

            var fileOpenService = new Mock<IFileOpenService>();
            fileOpenService.Setup(t => t.OpenFile()).Returns(filePath);

            var fileWatcher = new Mock<IFileWatcher>();

            var viewModel = new MainViewModel(fileOpenService.Object, fileWatcher.Object);
            viewModel.OpenFileCommand.Execute(null);

            fileWatcher.VerifyNoOtherCalls();
        }
    }
}