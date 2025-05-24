using CommunityToolkit.Mvvm.Input;
using Stofipy.App.Services;

namespace Stofipy.App.ViewModels.Global;

public partial class SectionTopVM(INavigationService navigationService) : ViewModelBase
{
    [RelayCommand]
    private Task GoToHome()
    {
        navigationService.NavigateToHome();
        return Task.CompletedTask;
    }
}