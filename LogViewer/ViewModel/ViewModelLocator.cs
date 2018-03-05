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
using LogViewer.Tokens;
using System.Collections.Generic;

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
            return new TokenConfiguration(new List<Token>
            {
                new LogLevelToken(),
                new DateToken()
            });
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}