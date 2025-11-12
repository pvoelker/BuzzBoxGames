using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Reflection;

namespace BuzzBoxGames.ViewModel
{
    public partial class MainMenu : ObservableValidator
    {
        public MainMenu()
        {
            var assembly = Assembly.GetEntryAssembly();

            if (assembly != null)
            {
                var assemblyName = assembly.GetName();
                if(assemblyName != null && assemblyName.Version != null)
                {
                    Version = assemblyName.Version;
                }
                else
                {
                    Version = null;
                }

                object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), true);
                if (attributes.Length > 0)
                {
                    Copyright = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
                }
                else
                {
                    Copyright = "ERROR: Unable to get copyright";
                }
            }
            else
            {
                Version = AppInfo.Current.Version;
                Copyright = null;
            }
        }

        #region Commands

        #endregion

        public bool IsVersionInfoPresent
        {
            get => Version != null;
        }

        private bool _autoRestart = true;
        public bool AutoRestart
        {
            get => _autoRestart;
            set => SetProperty(ref _autoRestart, value);
        }

        private Version? _version;
        public Version? Version
        {
            get => _version;
            private set
            {
                SetProperty(ref _version, value);
                OnPropertyChanged(nameof(IsVersionInfoPresent));
            }
        }

        private string? _copyright;
        public string? Copyright
        {
            get => _copyright;
            private set => SetProperty(ref _copyright, value);
        }
    }
}
