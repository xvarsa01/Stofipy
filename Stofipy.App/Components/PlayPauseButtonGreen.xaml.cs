using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stofipy.App.Components;

public partial class PlayPauseButtonGreen : ContentView
{
    public PlayPauseButtonGreen()
    {
        InitializeComponent();
    }
    
    public static readonly BindableProperty ButtonPressedCommandProperty =
        BindableProperty.Create(nameof(ButtonPressedCommand), typeof(ICommand), typeof(PlayPauseButtonGreen));
    public ICommand ButtonPressedCommand
    {
        get => (ICommand)GetValue(ButtonPressedCommandProperty);
        set => SetValue(ButtonPressedCommandProperty, value);
    }
    
    public static readonly BindableProperty MusicIsPlayingProperty =
        BindableProperty.Create(
            nameof(MusicIsPlaying),
            typeof(bool),
            typeof(PlayPauseButtonGreen),
            false,
            propertyChanged: OnMusicIsPlayingChanged);

    public bool MusicIsPlaying
    {
        get => (bool)GetValue(MusicIsPlayingProperty);
        set => SetValue(MusicIsPlayingProperty, value);
    }

    public static readonly BindableProperty ImageIconProperty =
        BindableProperty.Create(
            nameof(ImageIcon),
            typeof(string),
            typeof(PlayPauseButtonGreen),
            "play_green.png");

    public string ImageIcon
    {
        get => (string)GetValue(ImageIconProperty);
        private set => SetValue(ImageIconProperty, value);
    }

    private static void OnMusicIsPlayingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (PlayPauseButtonGreen)bindable;
        control.UpdateImageIcon();
    }

    private void UpdateImageIcon()
    {
        ImageIcon = MusicIsPlaying ? "pause.png" : "play_green.png";
    }

}