using BuzzBoxGamesApp.Services;

namespace BuzzBoxGamesApp.Game;

public partial class DropTacToe : ContentPage
{
	public DropTacToe()
	{
		InitializeComponent();

        if (BindingContext is BuzzBoxGames.ViewModel.Game.DropTacToe context)
        {
            context.MessageBoxService = new MessageBoxService(this);
        }
    }

    private void ContentPage_Unloaded(object sender, EventArgs e)
    {
        var context = BindingContext as BuzzBoxGames.ViewModel.Game.DropTacToe;

        if (context != null)
        {
            context.EndGame?.Execute(null);
        }
    }
}