using System.Windows.Input;

namespace Stofipy.App.Components.Buttons;

public partial class Queue_RecentlyPlayed : ContentView
{
    public Queue_RecentlyPlayed()
    {
        InitializeComponent();
    }
    
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(HomepageRounded));
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public static readonly BindableProperty TapCommandProperty =
        BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(LikedSongs));
    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }
    
    public static readonly BindableProperty IsPressedProperty =
        BindableProperty.Create(nameof(IsPressed), typeof(bool), typeof(LikedSongs));
    public bool IsPressed
    {
        get => (bool)GetValue(IsPressedProperty);
        set => SetValue(IsPressedProperty, value);
    }
}