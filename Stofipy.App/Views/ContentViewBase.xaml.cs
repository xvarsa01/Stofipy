using Stofipy.App.ViewModels;

namespace Stofipy.App.Views;

public partial class ContentViewBase
{
    protected IViewModel ViewModel { get; }

    public ContentViewBase(IViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = ViewModel = viewModel;
        Loaded += async (s, e) => await ViewModel.OnAppearingAsync();
    }
}
