using BuzzBoxGamesApp.Services;
using SkiaSharp.Extended.UI.Controls;
using SkiaSharp.Extended.UI.Controls.Converters;

namespace BuzzBoxGamesApp.Game;

public partial class SimonSays : ContentPage
{
    private static SKLottieImageSourceConverter _lottieConverter = new SKLottieImageSourceConverter();

    public SimonSays(bool autoRestart)
	{
		InitializeComponent();

        BindingContext = new BuzzBoxGames.ViewModel.Game.SimonSays(autoRestart);

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

    private void _confetti_AnimationCompleted(object sender, EventArgs e)
    {
        var context = BindingContext as BuzzBoxGames.ViewModel.Game.SimonSays;

        if (context != null)
        {
            // PEV - 12/6/2023 - Don't know why this is needed, however if this is NOT done the animation won't play more than once...
            _confetti.Source = (SKLottieImageSource?)_lottieConverter.ConvertFromString("confetti.json");

            context.NextSequence.Execute(null);
        }
    }
}