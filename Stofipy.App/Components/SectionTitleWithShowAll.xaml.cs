using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stofipy.App.Components;

public partial class SectionTitleWithShowAll
{
    public SectionTitleWithShowAll()
    {
        InitializeComponent();
    }
    
    public static readonly BindableProperty TitleTextProperty =
        BindableProperty.Create(nameof(TitleText), typeof(string), typeof(SectionTitleWithShowAll));
    public string TitleText
    {
        get => (string)GetValue(TitleTextProperty);
        set => SetValue(TitleTextProperty, value);
    }
    
    public static readonly BindableProperty RightTextProperty =
        BindableProperty.Create(nameof(RightText), typeof(string), typeof(SectionTitleWithShowAll));
    public string RightText
    {
        get => (string)GetValue(RightTextProperty);
        set => SetValue(RightTextProperty, value);
    }
    
    
    public static readonly BindableProperty ShowAllCommandProperty =
        BindableProperty.Create(nameof(ShowAllCommand), typeof(ICommand), typeof(SectionTitleWithShowAll));

    public ICommand ShowAllCommand
    {
        get => (ICommand)GetValue(ShowAllCommandProperty);
        set => SetValue(ShowAllCommandProperty, value);
    }
    
}