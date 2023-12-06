using System.Text;

namespace BuzzBoxGamesApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Game.SimonSays());
        }

        private async void Button2_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Game.ReactionTime());
        }

        private async void Button3_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Game.DropTacToe());
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

                await DisplayAlert("About Game", builder.ToString(), "OK");
            }
            else
            {
                throw new NullReferenceException("Context cannot be null");
            }
        }
    }
}
