using CommunityToolkit.Mvvm.ComponentModel;

namespace BuzzBoxGames.ViewModel
{
    public class InputMonitoringWarn : ObservableObject
    {
        public InputMonitoringWarn()
        {
            _showWarning = Preferences.Default.Get(PreferenceKeys.ShowMacOSInputMonitoringWarning, true);
        }

        #region Commands

        // No commands

        #endregion

        private bool _showWarning;
        /// <summary>
        /// True to continue to show the warning, otherwise false
        /// </summary>
        public bool ShowWarning
        {
            get => _showWarning;
            set
            {
                Preferences.Default.Set(PreferenceKeys.ShowMacOSInputMonitoringWarning, value);
                SetProperty(ref _showWarning, value);
            }
        }
    }
}
