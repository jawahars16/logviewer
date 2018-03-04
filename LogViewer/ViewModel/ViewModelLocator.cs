/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:LogViewer"
                           x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using LogViewer.Core;
using LogViewer.Services;
using LogViewer.Services.Implementation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace LogViewer.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IFileOpenService, FileOpenService>();
            SimpleIoc.Default.Register<IFileWatcher, FileWatcher>();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register(InitializeTokens);
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public TokenConfiguration InitializeTokens()
        {
            var levelToken = new Token(new Regex("(info|error|warn|debug)", RegexOptions.Compiled | RegexOptions.IgnoreCase));
            levelToken.Styles = new List<TokenStyle>
            {
                new TokenStyle(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red)),
                new TokenStyle(TextElement.FontWeightProperty, FontWeights.Bold)
            };

            return new TokenConfiguration(new List<Token> { levelToken });
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}