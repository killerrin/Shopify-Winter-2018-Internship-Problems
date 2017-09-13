using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using KillerrinStudiosToolkit.Services;

namespace OrderViewer_UWP.ViewModels
{
    public abstract class AppViewModelBase : ViewModelBase
    {
        #region Services
        public bool CanNavigate { get { return SimpleIoc.Default.IsRegistered<NavigationService>(); } }
        public NavigationService NavigationService
        {
            get
            {
                return SimpleIoc.Default.GetInstance<NavigationService>(); ;
            }
        }

        public bool HasProgressService { get { return SimpleIoc.Default.IsRegistered<ProgressService>(); } }
        public ProgressService ProgressService
        {
            get
            {
                return SimpleIoc.Default.GetInstance<ProgressService>(); ;
            }
        }

        public bool HasMediaService { get { return SimpleIoc.Default.IsRegistered<MediaService>(); } }
        public MediaService MediaService
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MediaService>(); ;
            }
        }

        public bool HasSplitViewService { get { return SimpleIoc.Default.IsRegistered<SplitViewService>(); } }
        public SplitViewService SplitViewService
        {
            get
            {
                return SimpleIoc.Default.GetInstance<SplitViewService>(); ;
            }
        }

        public static string m_topNavBarText = "";
        public string TopNavBarText
        {
            get { return m_topNavBarText; }
            set
            {
                m_topNavBarText = value;
                RaisePropertyChanged(nameof(TopNavBarText));
            }
        }

        #endregion

        public AppViewModelBase()
        {
        }


        public abstract void Loaded();
        public abstract void OnNavigatedTo();
        public abstract void OnNavigatedFrom();
        public abstract void ResetViewModel();

        public override void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null) return;

            try
            {
                base.RaisePropertyChanged(propertyName);
            }
            catch (Exception) { }
        }
    }
}
