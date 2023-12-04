using BuzzBoxGamesApp.Services;

namespace BuzzBoxGamesApp.Game;

public partial class ReactionTime : ContentPage
{
	public ReactionTime()
	{
		InitializeComponent();

        if (BindingContext is BuzzBoxGames.ViewModel.Game.ReactionTime context)
        {
            context.MessageBoxService = new MessageBoxService(this);
        }
    }

    private void ContentPage_Unloaded(object sender, EventArgs e)
    {
        var context = BindingContext as BuzzBoxGames.ViewModel.Game.SimonSays;

        if (context != null)
        {
            //context.EndGame?.Execute(null);
        }
    }
}