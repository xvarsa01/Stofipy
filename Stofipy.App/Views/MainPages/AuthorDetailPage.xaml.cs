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
    private void OnPointerEntered(object sender, PointerEventArgs e)
    {
        if (sender is Border border && border.BindingContext is FileListModel model)
            model.IsHovered = true;
    }

    private void OnPointerExited(object sender, PointerEventArgs e)
    {
        if (sender is Border border && border.BindingContext is FileListModel model)
            model.IsHovered = false;
    }
}