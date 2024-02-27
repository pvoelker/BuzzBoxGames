namespace BuzzBoxGamesApp.Game;

/// <summary>
/// Reaction Time label control for a paddle
/// </summary>
public partial class ReactionTimePaddleLabel : ContentView
{
	public ReactionTimePaddleLabel()
	{
        InitializeComponent();

        _grid.BindingContext = this;
    }

    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(BuzzBoxGames.ViewModel.Game.ReactionTimePaddle), typeof(ReactionTimePaddleLabel), null, BindingMode.OneWay);

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ReactionTimePaddleLabel), Colors.Magenta);

    public BuzzBoxGames.ViewModel.Game.ReactionTimePaddle Value
    {
        get => (BuzzBoxGames.ViewModel.Game.ReactionTimePaddle)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }
}