using BuzzBoxGamesApp.Services;
using CommunityToolkit.Maui.Views;

namespace BuzzBoxGamesApp.Game;

public partial class SimonSays : ContentPage
{
	public SimonSays()
	{
		InitializeComponent();

        if (BindingContext is BuzzBoxGames.ViewModel.Game.SimonSays context)
        {
            context.MessageBoxService = new MessageBoxService(this);
        }
    }

    private void ContentPage_Unloaded(object sender, EventArgs e)
    {
        var context = BindingContext as BuzzBoxGames.ViewModel.Game.SimonSays;

        if (context != null)
        {
            context.EndGame?.Execute(null);
        }
    }
}