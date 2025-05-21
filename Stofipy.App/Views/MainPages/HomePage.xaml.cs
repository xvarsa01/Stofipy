using Stofipy.App.ViewModels;

namespace Stofipy.App.Views.MainPages;

public partial class HomePage
{
    public HomePage(ListOfPlaylistsVM viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
    
}