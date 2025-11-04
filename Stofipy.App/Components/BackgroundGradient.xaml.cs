using Stofipy.App.Helpers;

namespace Stofipy.App.Components;

public partial class BackgroundGradient
{
    public BackgroundGradient()
    {
        InitializeComponent();
    }

    public Color Average { get; set; } = Colors.Gray;
    
    public string Picture
    {
        get => (string)GetValue(PictureProperty);
        set => SetValue(PictureProperty, value);
    }
    public static readonly BindableProperty PictureProperty = BindableProperty.Create(nameof(Picture), typeof(string), typeof(BackgroundGradient), propertyChanged: OnPictureChanged);
    
    private static async void OnPictureChanged(BindableObject bindable, object oldValue, object newValue)
    {

        try
        {
            var control = (BackgroundGradient)bindable;
            var helper = new ImageToColorHelper();
            
            var imageSource = ImageSource.FromFile((string)newValue);
            var avgColor = await helper.GetMostDominantColor(imageSource);
            control.Average = avgColor;
        }
        catch
        {
            // ignored
        }
    }
}