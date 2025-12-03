using System.Text;

namespace BuzzBoxGamesApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
# if DEBUG
            System.Diagnostics.Debug.WriteLine("DEBUG: macOS Input Monitoring Warning setting reset");
            Preferences.Default.Set(BuzzBoxGames.ViewModel.PreferenceKeys.ShowMacOSInputMonitoringWarning, true);
# endif

            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var context = BindingContext as BuzzBoxGames.ViewModel.MainMenu;

            if (context != null)
            {
				await Navigation.PushAsync(GetNextPage(new Game.SimonSays(context.AutoRestart)));
            }
        }

        private async void Button2_Clicked(object sender, EventArgs e)
        {
            var context = BindingContext as BuzzBoxGames.ViewModel.MainMenu;

            if (context != null)
            {
				await Navigation.PushAsync(GetNextPage(new Game.ReactionTime(context.AutoRestart)));
            }
        }

        private async void Button3_Clicked(object sender, EventArgs e)
        {
            var context = BindingContext as BuzzBoxGames.ViewModel.MainMenu;

            if (context != null)
            {
				await Navigation.PushAsync(GetNextPage(new Game.DropTacToe(context.AutoRestart)));
            }
        }

        private static ContentPage GetNextPage(ContentPage gamePage)
        {
            if (DeviceInfo.Current.Platform == DevicePlatform.MacCatalyst)
            {
                if(Preferences.Default.Get(BuzzBoxGames.ViewModel.PreferenceKeys.ShowMacOSInputMonitoringWarning, true))
                {
                    return new InputMonitoringWarnPage(gamePage);
                }
                else
                {
                    return gamePage;
                }
            }
            else
            {
                return gamePage;
            }            
        }

        private async void InfoButton_Clicked(object sender, EventArgs e)
        {
            var context = BindingContext as BuzzBoxGames.ViewModel.MainMenu;

            if (context != null)
            {
                var builder = new StringBuilder();
                builder.AppendLine("Buzz Box Games");
                if (context.Copyright != null)
                {
                    builder.AppendLine(context.Copyright);
                }
                builder.AppendLine("MIT License");
                if (context.Version != null)
                {
                    builder.AppendLine($"Version: {context.Version}");
                }

                builder.AppendLine(string.Empty);
                builder.AppendLine("• Works with Brian's Boxes USB quiz sets");
                builder.AppendLine("• Given to the glory of God");

                await DisplayAlertAsync("About Game", builder.ToString(), "OK");
            }
            else
            {
                throw new NullReferenceException("Context cannot be null");
            }
        }
    }
}
