using System.Windows.Input;

namespace Stofipy.App.Components.Menubar;

public partial class MenuItem : ContentView
{
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
    
    public static readonly BindableProperty IsHoveredProperty =
        BindableProperty.Create(nameof(IsHovered), typeof(bool), typeof(FileWithPictureAnd2Texts));
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
}