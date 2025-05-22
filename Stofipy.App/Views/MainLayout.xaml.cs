using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stofipy.App.ViewModels;
using Stofipy.App.ViewModels.Author;
using Stofipy.App.Views.MainPages;

namespace Stofipy.App.Views;

public partial class MainLayout : ContentPage
{
    public MainLayout( FilesInQueueVM filesInQueueVM, ListOfPlaylistsVM listOfPlaylistsVM, PlaylistDetailVM playlistDetailVM, AuthorDetailVM authorDetailVM)
    {
        InitializeComponent();
        
        LoadSectionTop(new SectionTop());
        LoadSectionBottom(new SectionBottom(filesInQueueVM));
        LoadSectionLeft(new SectionLeft(listOfPlaylistsVM));
        LoadSectionRight(new SectionRight(filesInQueueVM));
        
        // LoadSectionMiddle(new HomePage(listOfPlaylistsVM));
        // LoadSectionMiddle(new PlaylistDetailPage(playlistDetailVM));
        LoadSectionMiddle(new AuthorDetailPage(authorDetailVM));
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
}