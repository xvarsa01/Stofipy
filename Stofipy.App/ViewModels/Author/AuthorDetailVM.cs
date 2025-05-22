using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels.Author;

[QueryProperty(nameof(Id), nameof(Id))]
public class AuthorDetailVM(IAuthorFacade facade) : ViewModelBase
{
    public Guid Id { get; set; } = Guid.Parse("63E304C0-F051-436D-A466-3048C1C0D31F");
    public AuthorDetailModel? Author { get; set; }
    public List<FileListModel> PopularFiles { get; set; } = [];
    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Author = await facade.GetByIdAsync(Id);
        PopularFiles = Author.Files.Take(5).ToList();
    }
}