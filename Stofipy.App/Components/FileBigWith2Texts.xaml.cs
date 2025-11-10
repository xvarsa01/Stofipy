using System.Windows.Input;

namespace Stofipy.App.Components;

public partial class FileBigWith2Texts : ContentView
{
    private bool _isPointerOver;
    
    public FileBigWith2Texts()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty IsInteractiveProperty =
        BindableProperty.Create(
            nameof(IsInteractive),
            typeof(bool),
            typeof(FileWithPictureAnd1Text),
            true, // default: interactive
            propertyChanged: OnIsInteractiveChanged);

    public bool IsInteractive
    {
        get => (bool)GetValue(IsInteractiveProperty);
        set => SetValue(IsInteractiveProperty, value);
    }

    private static void OnIsInteractiveChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is FileWithPictureAnd1Text control && newValue is bool isInteractive)
        {
            control.InputTransparent = !isInteractive;
        }
    }
    
    public static readonly BindableProperty PlayCommandProperty =
        BindableProperty.Create(nameof(PlayCommand), typeof(ICommand), typeof(FileBigWith2Texts));
    public ICommand PlayCommand
    {
        get => (ICommand)GetValue(PlayCommandProperty);
        set => SetValue(PlayCommandProperty, value);
    }
    
    public static readonly BindableProperty TapCommandProperty =
        BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(FileBigWith2Texts));
    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(FileBigWith2Texts));

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    
    
    public static readonly BindableProperty PictureProperty =
        BindableProperty.Create(nameof(Picture), typeof(ImageSource), typeof(FileBigWith2Texts));

    public ImageSource Picture
    {
        get => (ImageSource)GetValue(PictureProperty);
        set => SetValue(PictureProperty, value);
    }

    public static readonly BindableProperty TopTextProperty =
        BindableProperty.Create(nameof(TopText), typeof(string), typeof(FileBigWith2Texts));

    public string TopText
    {
        get => (string)GetValue(TopTextProperty);
        set => SetValue(TopTextProperty, value);
    }

    public static readonly BindableProperty BottomTextProperty =
        BindableProperty.Create(nameof(BottomText), typeof(string), typeof(FileBigWith2Texts));

    public string BottomText
    {
        get => (string)GetValue(BottomTextProperty);
        set => SetValue(BottomTextProperty, value);
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
        if (_isPointerOver)
            ApplyVisualStyles(Color.FromArgb("#333333"),true);
        else
            ApplyVisualStyles(Color.FromArgb("#00000000"));
    }

    private void ApplyVisualStyles(Color bgColor, bool areChildrenVisible = false)
    {
        MainBorder.BackgroundColor = bgColor;
        PlayButton.IsVisible = areChildrenVisible;
    }
}