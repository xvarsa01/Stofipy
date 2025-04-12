namespace Stofipy.App.Views.MainPages;

public partial class PlaylistDetailWrapperPage : MainLayout
{
    public PlaylistDetailWrapperPage()
    {
        InitializeComponent();
        LoadSectionMiddle(new PlaylistDetailPage());
    }
}