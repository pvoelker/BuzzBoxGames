using System;

namespace BuzzBoxGamesApp.Game;

/// <summary>
/// Vertical data bar
/// </summary>
public partial class VerticalDataBar : ContentView
{
	public VerticalDataBar()
	{
        InitializeComponent();

        _grid.BindingContext = this;
    }

    public static readonly BindableProperty MaxProperty = BindableProperty.Create(nameof(Max), typeof(double), typeof(VerticalDataBar), 100.0, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is VerticalDataBar gb)
        {
            CalculateBarHeight(gb);
        }
    });
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(VerticalDataBar), 7.5, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is VerticalDataBar gb)
        {
            CalculateBarHeight(gb);
        }
    });
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(VerticalDataBar), "OpenSansRegular");
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(VerticalDataBar), 24.0);
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(VerticalDataBar), String.Empty);
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(VerticalDataBar), Colors.Black);
    public static new readonly BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Brush), typeof(VerticalDataBar), Brush.Magenta);
    public static readonly BindableProperty BarProperty = BindableProperty.Create(nameof(Bar), typeof(Brush), typeof(VerticalDataBar), Brush.Cyan);

    public double Max
    {
        get => (double)GetValue(MaxProperty);
        set => SetValue(MaxProperty, value);
    }

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
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

    public Brush Bar
    {
        get => (Brush)GetValue(BarProperty);
        set => SetValue(BarProperty, value);
    }

    protected override Size ArrangeOverride(Rect bounds)
    {
        CalculateBarHeight(this);

        return base.ArrangeOverride(bounds);
    }

    static protected void CalculateBarHeight(VerticalDataBar gb)
    {
        if (gb._bar != null)
        {
            var percentage = gb.Value / gb.Max;

            gb._bar.HeightRequest = percentage * gb._bkbar.Height;
        }
    }
}