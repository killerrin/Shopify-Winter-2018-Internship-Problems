using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System.Diagnostics;
using System;
using Windows.UI.Xaml;
using System.Threading.Tasks;
using Windows.UI.Popups;
using KillerrinStudiosToolkit.Helpers;
using OrderViewer_UWP.Models.Enums;

namespace OrderViewer_UWP.ViewModels
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
    public class MainViewModel : AppViewModelBase
    {
        public static MainViewModel Instance {  get { return ServiceLocator.Current.GetInstance<MainViewModel>(); } }
        public VisualState CurrentVisualState;

        #region Properties
        private NavigationLocation m_currentNavigationLocation = NavigationLocation.None;
        public NavigationLocation CurrentNavigationLocation
        {
            get { return m_currentNavigationLocation; }
            set
            {
                if (m_currentNavigationLocation == value) return;
                m_currentNavigationLocation = value;
                RaisePropertyChanged(nameof(CurrentNavigationLocation));

                TopNavBarText = StringHelpers.AddSpacesToSentence(m_currentNavigationLocation.ToString(), true);
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
            : base()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }

            ResetViewModel();
        }

        public override void Loaded()
        {   
            // Handle Launch Args

        }

        public override void OnNavigatedTo()
        {
        }

        public override void OnNavigatedFrom()
        {

        }

        public override void ResetViewModel()
        {

        }

        #region Navigation Commands
        //public RelayCommand NavigateHomeCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(() =>
        //        {
        //            if (!CanNavigate)
        //                return;

        //            MainViewModel.Instance.CurrentNavigationLocation = Models.Enums.NavigationLocation.Home;
        //            NavigationService.Navigate(typeof(HomePage), null);
        //        });
        //    }
        //}
        #endregion
    }
}