namespace Stofipy.App.Components.Queue;

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