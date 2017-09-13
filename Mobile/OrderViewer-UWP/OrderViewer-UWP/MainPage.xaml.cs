using GalaSoft.MvvmLight.Ioc;
using KillerrinStudiosToolkit.Services;
using OrderViewer_UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using KillerrinStudiosToolkit.Controls;
using OrderViewer_UWP.Models.Enums;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OrderViewer_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Instance;
        public MainViewModel ViewModel { get { return (MainViewModel)DataContext; } }

        public ProgressIndicator MainProgressIndicator { get; private set; }

        public MainPage()
        {
            Instance = this;
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Finally, send the loading event
            ViewModel.Loaded();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.OnNavigatedTo();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel.OnNavigatedFrom();
            base.OnNavigatedFrom(e);
        }

        #region Unavoidable Control Events 
        private void VisualStateGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            Debug.WriteLine($"{nameof(VisualStateGroup_CurrentStateChanged)}");
            ViewModel.CurrentVisualState = e.NewState;
        }
        #endregion

        #region Loaded

        private void MainProgressIndicator_Loaded(object sender, RoutedEventArgs e)
        {
            if (!SimpleIoc.Default.IsRegistered<ProgressService>())
            {
                SimpleIoc.Default.Register<ProgressService>(() => { return new ProgressService(MainProgressIndicator); });
            }
        }
        #endregion
    }
}
