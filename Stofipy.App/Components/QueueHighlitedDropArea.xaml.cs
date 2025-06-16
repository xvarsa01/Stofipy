using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stofipy.App.Components;

public partial class QueueHighlightedDropArea : ContentView
{
    public QueueHighlightedDropArea()
    {
        InitializeComponent();
    }
    
    public static readonly BindableProperty HighlightedProperty =
        BindableProperty.Create(nameof(Highlighted), typeof(bool), typeof(QueueHighlightedDropArea));

    public bool Highlighted
    {
        get => (bool)GetValue(HighlightedProperty);
        set => SetValue(HighlightedProperty, value);
    }
}