using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stofipy.App.Views.MainPages;

namespace Stofipy.App.Views;

public partial class MainLayout : ContentPage
{
    public MainLayout()
    {
        InitializeComponent();
        
        LoadSectionTop(new SectionTop());
        LoadSectionBottom(new SectionBottom());
        LoadSectionLeft(new SectionLeft());
        LoadSectionRight(new SectionLeft());
        
        LoadSectionMiddle(new HomePage());
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