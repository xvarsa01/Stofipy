using System.Windows.Input;

namespace Stofipy.App.Components.Menubar;

public partial class MenuItem : ContentView
{
    private bool _isPointerOver;

    public MenuItem()
    {
        InitializeComponent();
    }
    
    public static readonly BindableProperty IconNameProperty =
        BindableProperty.Create(nameof(IconName), typeof(string), typeof(MenuItem));
    public string IconName
    {
        get => (string)GetValue(IconNameProperty);
        set => SetValue(IconNameProperty, value);
    }
    
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(MenuItem));
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public static readonly BindableProperty TapCommandProperty =
        BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(FileWithPictureAnd1Text));
    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }
    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(FileWithPictureAnd1Text));

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    
    private void OnPointerEntered(object sender, PointerEventArgs e)
    {
        _isPointerOver = true;
        UpdateVisualState();
    }

    private void OnPointerExited(object sender, PointerEventArgs e)
    {
        _isPointerOver = false;
        UpdateVisualState();
    }
    
    private void UpdateVisualState()
    {
        if (_isPointerOver)
            ApplyVisualStyles(Color.FromArgb("#333333"));
        else
            ApplyVisualStyles(Color.FromArgb("#00000000"));
    }
    
    private void ApplyVisualStyles(Color bgColor)
    {
        MainBorder.BackgroundColor = bgColor;
    }
}