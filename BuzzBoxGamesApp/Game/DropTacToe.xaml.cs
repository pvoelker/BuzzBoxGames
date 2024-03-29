using BuzzBoxGamesApp.Services;
using SkiaSharp.Extended.UI.Controls;
using SkiaSharp.Extended.UI.Controls.Converters;

namespace BuzzBoxGamesApp.Game;

public partial class DropTacToe : ContentPage
{
    private static SKLottieImageSourceConverter _lottieConverter = new SKLottieImageSourceConverter();

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

    private void _confetti_AnimationCompleted(object sender, EventArgs e)
    {
        var context = BindingContext as BuzzBoxGames.ViewModel.Game.DropTacToe;

        if (context != null)
        {
            // PEV - 12/6/2023 - Don't know why this is needed, however if this is NOT done the animation won't play more than once...
            _confetti.Source = (SKLottieImageSource?)_lottieConverter.ConvertFromString("cool_fireworks.json");

            context.NextRound.Execute(null);
        }
    }
}