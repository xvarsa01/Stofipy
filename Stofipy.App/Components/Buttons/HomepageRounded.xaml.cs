using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stofipy.App.Components.Buttons;

public partial class HomepageRounded : ContentView
{
    public HomepageRounded()
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
        BindableProperty.Create(nameof(TapCommand), typeof(ICommand), typeof(HomepageRounded));
    public ICommand TapCommand
    {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }
    
    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(HomepageRounded));
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    
    public static readonly BindableProperty SelectedProperty =
        BindableProperty.Create(nameof(Selected), typeof(bool), typeof(HomepageRounded));
    public bool Selected
    {
        get => (bool)GetValue(SelectedProperty);
        set => SetValue(SelectedProperty, value);
    }
}