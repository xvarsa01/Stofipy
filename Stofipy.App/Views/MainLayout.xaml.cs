using Stofipy.App.Services;
using Stofipy.App.ViewModels;
using Stofipy.App.ViewModels.Author;
using Stofipy.App.ViewModels.Global;
using Stofipy.App.Views.MainPages;
using NavigationRequest = Stofipy.App.Services.NavigationRequest;

namespace Stofipy.App.Views;

public partial class MainLayout : ContentPage
{
    private readonly ListOfPlaylistsVM _listOfPlaylistsVM;

    public MainLayout( FilesInQueueVM filesInQueueVM, ListOfPlaylistsVM listOfPlaylistsVM,
        PlaylistDetailVM playlistDetailVM, AuthorDetailVM authorDetailVM, SectionTopVM sectionTopVM,
        INavigationService navigationService)
    {
        _listOfPlaylistsVM = listOfPlaylistsVM;
        InitializeComponent();
        
        LoadSectionTop(new SectionTop(sectionTopVM));
        LoadSectionBottom(new SectionBottom(filesInQueueVM));
        LoadSectionLeft(new SectionLeft(listOfPlaylistsVM));
        LoadSectionRight(new SectionRight(filesInQueueVM));
        
        // LoadSectionMiddle(new HomePage(listOfPlaylistsVM));
        // LoadSectionMiddle(new PlaylistDetailPage(playlistDetailVM));
        LoadSectionMiddle(new AuthorDetailPage(authorDetailVM));

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
    
    private void OnNavigationRequested(NavigationRequest request)
    {
        switch (request)
        {
            case NavigateToHomeRequest:
                LoadSectionMiddle(new HomePage(_listOfPlaylistsVM));
                break;

            case NavigateToAuthorRequest authorRequest:
                LoadSectionMiddle(new AuthorDetailPage(authorRequest.ViewModel));
                break;

            case NavigateToPlaylistRequest playlistRequest:
                LoadSectionMiddle(new PlaylistDetailPage(playlistRequest.ViewModel));
                break;
        }
    }
}