using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Stofipy.App.Messages;

namespace Stofipy.App.Components.Queue;

public partial class HoverableComponentInQueue : IRecipient<MediaElementPlayMessage>, IRecipient<MediaElementPauseMessage>
{
    public HoverableComponentInQueue()
    {
        var messenger = App.Services.GetRequiredService<IMessenger>();
        messenger.Register<MediaElementPlayMessage>(this);
        messenger.Register<MediaElementPauseMessage>(this);
        InitializeComponent();
    }
    
    public static readonly BindableProperty TapCommandProperty =
        BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(HoverableComponentInQueue));
    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(HoverableComponentInQueue));
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    
    public static readonly BindableProperty PlayClickedCommandProperty =
        BindableProperty.Create(nameof(PlayClickedCommand), typeof(ICommand), typeof(HoverableComponentInQueue));
    public ICommand PlayClickedCommand
    {
        get => (ICommand)GetValue(PlayClickedCommandProperty);
        set => SetValue(PlayClickedCommandProperty, value);
    }
    
    public static readonly BindableProperty DotsClickedCommandProperty =
        BindableProperty.Create(nameof(DotsClickedCommand), typeof(ICommand), typeof(HoverableComponentInQueue));
    public ICommand DotsClickedCommand
    {
        get => (ICommand)GetValue(DotsClickedCommandProperty);
        set => SetValue(DotsClickedCommandProperty, value);
    }
    
    public static readonly BindableProperty IsNowPlayingComponentProperty =
        BindableProperty.Create(nameof(IsNowPlayingComponent), typeof(bool), typeof(HoverableComponentInQueue), false);
    public bool IsNowPlayingComponent
    {
        get => (bool)GetValue(IsNowPlayingComponentProperty);
        set => SetValue(IsNowPlayingComponentProperty, value);
    }
    
    
    public static readonly BindableProperty PictureProperty =
        BindableProperty.Create(nameof(Picture), typeof(ImageSource), typeof(HoverableComponentInQueue));
    public ImageSource Picture
    {
        get => (ImageSource)GetValue(PictureProperty);
        set => SetValue(PictureProperty, value);
    }

    public static readonly BindableProperty TopTextProperty =
        BindableProperty.Create(nameof(TopText), typeof(string), typeof(HoverableComponentInQueue));
    public string TopText
    {
        get => (string)GetValue(TopTextProperty);
        set => SetValue(TopTextProperty, value);
    }

    public static readonly BindableProperty BottomTextProperty =
        BindableProperty.Create(nameof(BottomText), typeof(string), typeof(HoverableComponentInQueue));
    public string BottomText
    {
        get => (string)GetValue(BottomTextProperty);
        set => SetValue(BottomTextProperty, value);
    }
    
    
    public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(HoverableComponentInQueue));
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }
    
    public static readonly BindableProperty IsHoveredProperty =
        BindableProperty.Create(nameof(IsHovered), typeof(bool), typeof(HoverableComponentInQueue));
    public bool IsHovered
    {
        get => (bool)GetValue(IsHoveredProperty);
        set => SetValue(IsHoveredProperty, value);
    }

    
    private void OnPointerEntered(object sender, PointerEventArgs e)
    {
        IsHovered = true;
    }

    private void OnPointerExited(object sender, PointerEventArgs e)
    {
        IsHovered = false;
    }

    public bool IsPlaying { get; set; }
    public void Receive(MediaElementPlayMessage message)
    {
        IsPlaying = true;
    }

    public void Receive(MediaElementPauseMessage message)
    {
        IsPlaying = false;
    }
}