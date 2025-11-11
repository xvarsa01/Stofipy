using System.Windows.Input;
using Stofipy.App.Helpers;
using Stofipy.App.Resources;

namespace Stofipy.App.Components.Buttons;

public partial class SpChip
{
    public SpChip()
    {
        InitializeComponent();
        PropertyChanged += SpChip_PropertyChanged;
    }
    
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(SpChip));
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
        
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(SpChip));
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    
    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(SpChip));
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    
    public static readonly BindableProperty IsSelectedProperty =
        BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(SpChip), propertyChanged: IsSelected_OnChanged);
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }
    
    private static void IsSelected_OnChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not SpChip spChip
            || newValue is not bool newValueBool) return;
        if (newValueBool)
        {
            spChip.SetStyleSelected();
        }
        else
        {
            spChip.SetStyleUnselected();
        }
    }
    private void SpChip_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(IsEnabled)) return;
        UpdateStyle();
    }
    
    private void UpdateStyle()
    {
        switch (IsEnabled, IsSelected)
        {
            case (true, true): SetStyleSelected(); break;
            case (true, false): SetStyleUnselected(); break;
            case (false, true): SetStyleSelected(); break;
            case (false, _): SetStyleDisabled(); break;
        }
    }
    
    private void SetStyleSelected()
    {
        ChipBorder.BackgroundColor = ResourcesHelper.GetResourceValue(ColorKeys.Gray7Color);
        ChipText.TextColor = ResourcesHelper.GetResourceValue(ColorKeys.Gray2Color);
    }

    private void SetStyleUnselected()
    {
        ChipBorder.BackgroundColor = ResourcesHelper.GetResourceValue(ColorKeys.Gray2Color);
        ChipText.TextColor = ResourcesHelper.GetResourceValue(ColorKeys.Gray7Color);
    }
    
    private void SetStyleDisabled()
    {
        ChipBorder.BackgroundColor = ResourcesHelper.GetResourceValue(ColorKeys.Gray2Color);
        ChipText.TextColor = ResourcesHelper.GetResourceValue(ColorKeys.Gray7Color);
    }
}