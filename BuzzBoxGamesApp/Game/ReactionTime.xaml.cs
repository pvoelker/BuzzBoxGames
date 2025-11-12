using BuzzBoxGamesApp.Services;

namespace BuzzBoxGamesApp.Game;

public partial class ReactionTime : ContentPage
{
	public ReactionTime(bool autoRestart)
	{
        BindingContext = new BuzzBoxGames.ViewModel.Game.ReactionTime(autoRestart);
        
        InitializeComponent();

        if (BindingContext is BuzzBoxGames.ViewModel.Game.ReactionTime context)
        {
            context.MessageBoxService = new MessageBoxService(this);
        }
    }

    private void ContentPage_Unloaded(object sender, EventArgs e)
    {
        var context = BindingContext as BuzzBoxGames.ViewModel.Game.ReactionTime;

        context?.Dispose();
    }
}