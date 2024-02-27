namespace BuzzBoxGamesApp.Game;

/// <summary>
/// Reaction Time bar control for a paddle
/// </summary>
public partial class ReactionTimePaddle : ContentView
{
	public ReactionTimePaddle()
	{
        InitializeComponent();

        _grid.BindingContext = this;
    }

    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(BuzzBoxGames.ViewModel.Game.ReactionTimePaddle), typeof(ReactionTimePaddle), null, BindingMode.OneWay);

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ReactionTimePaddle), Colors.Magenta);
    public static readonly BindableProperty BarTextColorProperty = BindableProperty.Create(nameof(BarTextColor), typeof(Color), typeof(ReactionTimePaddle), Colors.DarkMagenta);

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

    public Color BarTextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }
}