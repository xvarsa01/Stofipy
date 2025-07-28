using Stofipy.App.Services;
using Stofipy.App.ViewModels;
using Stofipy.App.ViewModels.Album;
using Stofipy.App.ViewModels.Author;
using SectionTopVM = Stofipy.App.ViewModels.SectionTopVM;
using Stofipy.App.Views.MainPages;
using NavigationRequest = Stofipy.App.Services.NavigationRequest;

namespace Stofipy.App.Views;

public partial class MainLayout : ContentPage
{
    private readonly HomePageVM _homePageVM;
    private readonly IServiceProvider _serviceProvider;

    public MainLayout( FilesInQueueVM filesInQueueVM,
        HomePageVM homePageVM,
        SectionTopVM sectionTopVM,
        SectionLeftVM sectionLeftVM,
        INavigationService navigationService,
        IServiceProvider serviceProvider)
    {
        _homePageVM = homePageVM;
        _serviceProvider = serviceProvider;
        InitializeComponent();
        
        LoadSectionTop(new SectionTop(sectionTopVM));
        LoadSectionBottom(new SectionBottom(filesInQueueVM));
        LoadSectionLeft(new SectionLeft(sectionLeftVM));
        LoadSectionRight(new SectionRight(filesInQueueVM));
        
        LoadSectionMiddle(new HomePage(homePageVM));
        // LoadSectionMiddle(new PlaylistDetailPage(playlistDetailVM));
        // LoadSectionMiddle(new AuthorDetailPage(authorDetailVM));

        navigationService.NavigationRequested += OnNavigationRequested;
    }
    
    public void LoadSectionTop(View sectionTop)
    {
        TopContent.Content = sectionTop;
    }
    
    public void LoadSectionBottom(View sectionBottom)
    {
        BottomContent.Content = sectionBottom;
    }
    
    public void LoadSectionLeft(View sectionLeft)
    {
        LeftContent.Content = sectionLeft;
    }
    
    public void LoadSectionRight(View sectionRight)
    {
        RightContent.Content = sectionRight;
    }
    public void LoadSectionMiddle(View view)
    {
        CenterContent.Content = view;
    }
    
    private async Task OnNavigationRequested(NavigationRequest request)
    {
        switch (request)
        {
            case NavigateToHomeRequest:
                LoadSectionMiddle(new HomePage(_homePageVM));
                break;
            
            case NavigateToYourShowsRequest:
                break;
            
            case NavigateToMadeForYouRequest:
                break;
            
            case NavigateToRecentlyPlayedRequest:
                break;
            
            case NavigateToPopularRadioRequest:
                break;

            case NavigateToAuthorRequest authorRequest:
                var authorDetailVM = _serviceProvider.GetRequiredService<AuthorDetailVM>();
                await authorDetailVM.LoadByIdAsync(authorRequest.AuthorId);
                LoadSectionMiddle(new AuthorDetailPage(authorDetailVM));
                break;

            case NavigateToPlaylistRequest playlistRequest:
                var playlistDetailVM = _serviceProvider.GetRequiredService<PlaylistDetailVM>();
                await playlistDetailVM.LoadByIdAsync(playlistRequest.PlaylistId);
                LoadSectionMiddle(new PlaylistDetailPage(playlistDetailVM));
                break;
            
            case NavigateToAlbumRequest albumRequest:
                var albumDetailVM = _serviceProvider.GetRequiredService<AlbumDetailVM>();
                await albumDetailVM.LoadByIdAsync(albumRequest.AlbumId);
                LoadSectionMiddle(new AlbumDetailPage(albumDetailVM));
                break;
        }
    }
}