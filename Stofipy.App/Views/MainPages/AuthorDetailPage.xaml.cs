using Stofipy.App.ViewModels;
using Stofipy.App.ViewModels.Author;
using Stofipy.BL.Models;

namespace Stofipy.App.Views.MainPages;

public partial class AuthorDetailPage
{
    public AuthorDetailPage(AuthorDetailVM viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}