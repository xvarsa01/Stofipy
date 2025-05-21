using Stofipy.App.ViewModels;
using Stofipy.BL.Models;

namespace Stofipy.App.Views.MainPages;

public partial class PlaylistDetailPage
{
    public PlaylistDetailPage(PlaylistDetailVM viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
    private void OnPointerEntered(object sender, PointerEventArgs e)
    {
        if (sender is Border border && border.BindingContext is FilesInPlaylistModel model)
            model.IsHovered = true;
    }

    private void OnPointerExited(object sender, PointerEventArgs e)
    {
        if (sender is Border border && border.BindingContext is FilesInPlaylistModel model)
            model.IsHovered = false;
    }
}