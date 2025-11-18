using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Stofipy.App.Messages;

namespace Stofipy.App.Components;

public partial class RowOfAlbumComponent : IRecipient<MediaElementPlayMessage>, IRecipient<MediaElementPauseMessage>, IRecipient<RefreshQueueMessage>
{
    private bool _isPointerOver;
    
    public RowOfAlbumComponent()
    {
        var messenger = App.Services.GetRequiredService<IMessenger>();
        messenger.Register<MediaElementPlayMessage>(this);
        messenger.Register<MediaElementPauseMessage>(this);
        messenger.Register<RefreshQueueMessage>(this);
        InitializeComponent();
    }
    
    public static readonly BindableProperty ComponentIdProperty =
        BindableProperty.Create(nameof(ComponentId), typeof(Guid), typeof(UniversalHoverableComponent));
    public Guid ComponentId
    {
        get => (Guid)GetValue(ComponentIdProperty);
        set => SetValue(ComponentIdProperty, value);
    }
    
    public static readonly BindableProperty IndexTextProperty =
        BindableProperty.Create(nameof(IndexText), typeof(string), typeof(RowOfAlbumComponent));
    public string IndexText
    {
        get => (string)GetValue(IndexTextProperty);
        set => SetValue(IndexTextProperty, value);
        
    }
    
    public static readonly BindableProperty TopTextProperty =
        BindableProperty.Create(nameof(TopText), typeof(string), typeof(RowOfAlbumComponent));
    public string TopText
    {
        get => (string)GetValue(TopTextProperty);
        set => SetValue(TopTextProperty, value);
    }

    public static readonly BindableProperty BottomTextProperty =
        BindableProperty.Create(nameof(BottomText), typeof(string), typeof(RowOfAlbumComponent));
    public string BottomText
    {
        get => (string)GetValue(BottomTextProperty);
        set => SetValue(BottomTextProperty, value);
    }
    
    public static readonly BindableProperty PlayCountTextProperty =
        BindableProperty.Create(nameof(PlayCountText), typeof(string), typeof(RowOfAlbumComponent));
    public string PlayCountText
    {
        get => (string)GetValue(PlayCountTextProperty);
        set => SetValue(PlayCountTextProperty, value);
    }
    
    public static readonly BindableProperty DurationTextProperty =
        BindableProperty.Create(nameof(DurationText), typeof(string), typeof(RowOfAlbumComponent));
    public string DurationText
    {
        get => (string)GetValue(DurationTextProperty);
        set => SetValue(DurationTextProperty, value);
    }
    
    
    public static readonly BindableProperty TapCommandProperty =
        BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(RowOfAlbumComponent));
    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(RowOfAlbumComponent));
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    
    public static readonly BindableProperty BottomTextTapCommandProperty =
        BindableProperty.Create(nameof(BottomTextTapCommand), typeof(ICommand), typeof(RowOfAlbumComponent));
    public ICommand BottomTextTapCommand
    {
        get => (ICommand)GetValue(BottomTextTapCommandProperty);
        set => SetValue(BottomTextTapCommandProperty, value);
    }
    public static readonly BindableProperty PlayClickedCommandProperty =
        BindableProperty.Create(nameof(PlayClickedCommand), typeof(ICommand), typeof(RowOfAlbumComponent));
    public ICommand PlayClickedCommand
    {
        get => (ICommand)GetValue(PlayClickedCommandProperty);
        set => SetValue(PlayClickedCommandProperty, value);
    }
    
    public static readonly BindableProperty DotsClickedCommandProperty =
        BindableProperty.Create(nameof(DotsClickedCommand), typeof(ICommand), typeof(RowOfAlbumComponent));
    public ICommand DotsClickedCommand
    {
        get => (ICommand)GetValue(DotsClickedCommandProperty);
        set => SetValue(DotsClickedCommandProperty, value);
    }
    
    public static readonly BindableProperty IsNowPlayingComponentProperty =
        BindableProperty.Create(nameof(IsNowPlayingComponent), typeof(bool), typeof(RowOfAlbumComponent), false);
    public bool IsNowPlayingComponent
    {
        get => (bool)GetValue(IsNowPlayingComponentProperty);
        set => SetValue(IsNowPlayingComponentProperty, value);
    }
    
    
    public static readonly BindableProperty PictureProperty =
        BindableProperty.Create(nameof(Picture), typeof(ImageSource), typeof(RowOfAlbumComponent));
    public ImageSource Picture
    {
        get => (ImageSource)GetValue(PictureProperty);
        set => SetValue(PictureProperty, value);
    }


    
    
    public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(RowOfAlbumComponent), propertyChanged: OnIsSelectedChanged);
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }
    
    private static void OnIsSelectedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RowOfAlbumComponent control)
            control.UpdateVisualState();
    }
    
    void OnPointerEntered(object sender, PointerEventArgs e)
    {
        _isPointerOver = true;
        UpdateVisualState();
    }

    void OnPointerExited(object sender, PointerEventArgs e)
    {
        _isPointerOver = false;
        UpdateVisualState();
    }

    private void UpdateVisualState()
    {
        if (IsSelected)
            ApplyVisualStyles(Color.FromArgb("#555555"), true);
        else if (_isPointerOver)
            ApplyVisualStyles(Color.FromArgb("#333333"),true);
        else
            ApplyVisualStyles(Color.FromArgb("#00000000"));
    }

    private void ApplyVisualStyles(Color bgColor, bool areChildrenVisible = false)
    {
        MainBorder.BackgroundColor = bgColor;
        ShowMoreOptions.IsVisible = areChildrenVisible;
        PlayPauseIconOveIndexLabel.IsVisible = areChildrenVisible;
        IndexLabel.IsVisible = !areChildrenVisible;
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
    
    public void Receive(RefreshQueueMessage message)
    {
        IsNowPlayingComponent = message.FileId == ComponentId;
    }
}