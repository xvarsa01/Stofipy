using Stofipy.App.ViewModels;

namespace Stofipy.App.Views.MainPages;

public partial class HomePage
{
    public HomePage(ListOfPlaylistsViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
    
}