using System;

namespace BuzzBoxGamesApp.Game;

/// <summary>
/// Glow blocks for Simon Says
/// </summary>
public partial class GlowBlock : ContentView
{
	public GlowBlock()
	{
        InitializeComponent();

        _border.BindingContext = this;

        _shadow.Brush = HaloColor;
    }

    public static readonly BindableProperty IsLitProperty = BindableProperty.Create(nameof(IsLit), typeof(bool), typeof(GlowBlock), false);

    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(GlowBlock), "OpenSansRegular");
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(GlowBlock), 24.0);
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(GlowBlock), String.Empty);
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(GlowBlock), Colors.Black);
    public static new readonly BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Brush), typeof(GlowBlock), Brush.Magenta);
    public static readonly BindableProperty DarkBackgroundProperty = BindableProperty.Create(nameof(DarkBackground), typeof(Brush), typeof(GlowBlock), Brush.DarkMagenta);
    public static readonly BindableProperty HaloColorProperty = BindableProperty.Create(nameof(HaloColor), typeof(SolidColorBrush), typeof(GlowBlock), SolidColorBrush.Cyan, propertyChanged: (bindable, oldValue, newValue) =>
    {
        var gb = bindable as GlowBlock;

        if(gb != null)
        {
            gb._shadow.Brush = newValue as SolidColorBrush;
        }
    });

    public bool IsLit
    {
        get => (bool)GetValue(IsLitProperty);
        set => SetValue(IsLitProperty, value);
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

    public Brush DarkBackground
    {
        get => (Brush)GetValue(DarkBackgroundProperty);
        set => SetValue(DarkBackgroundProperty, value);
    }

    public SolidColorBrush HaloColor
    {
        get => (SolidColorBrush)GetValue(HaloColorProperty);
        set => SetValue(HaloColorProperty, value);
    }
}