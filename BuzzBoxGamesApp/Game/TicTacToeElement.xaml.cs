using BuzzBoxGames.ViewModel;
using System;

namespace BuzzBoxGamesApp.Game;

/// <summary>
/// Element on Tick Tac Toe board
/// </summary>
public partial class TicTacToeElement : ContentView
{
	public TicTacToeElement()
	{
        InitializeComponent();

        _grid.BindingContext = this;
    }

    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(TicTacToeEnum), typeof(TicTacToeElement), TicTacToeEnum.None);
    public static readonly BindableProperty IsWinnerProperty = BindableProperty.Create(nameof(IsWinner), typeof(bool), typeof(TicTacToeElement), false);

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(TicTacToeElement), Colors.Black);
    public static new readonly BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Brush), typeof(TicTacToeElement), Brush.SlateGray);
    public static readonly BindableProperty XBackgroundProperty = BindableProperty.Create(nameof(XBackground), typeof(Brush), typeof(TicTacToeElement), Brush.Red);
    public static readonly BindableProperty OBackgroundProperty = BindableProperty.Create(nameof(OBackground), typeof(Brush), typeof(TicTacToeElement), Brush.Green);
    public static readonly BindableProperty WinnerColorProperty = BindableProperty.Create(nameof(WinnerColor), typeof(Color), typeof(TicTacToeElement), Colors.Gold);

    public TicTacToeEnum Value
    {
        get => (TicTacToeEnum)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public bool IsWinner
    {
        get => (bool)GetValue(IsWinnerProperty);
        set => SetValue(IsWinnerProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public new Brush Background
    {
        get => (Brush)GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    public Brush XBackground
    {
        get => (Brush)GetValue(XBackgroundProperty);
        set => SetValue(XBackgroundProperty, value);
    }

    public Brush OBackground
    {
        get => (Brush)GetValue(OBackgroundProperty);
        set => SetValue(OBackgroundProperty, value);
    }

    public Color WinnerColor
    {
        get => (Color)GetValue(WinnerColorProperty);
        set => SetValue(WinnerColorProperty, value);
    }
}