using CommunityToolkit.Mvvm.Input;
using Stofipy.App.Services;
using Stofipy.App.Services.Interfaces;

namespace Stofipy.App.ViewModels;

public partial class SectionTopVM(
    INavigationService navigationService,
    IMessengerService messengerService) : ViewModelBase(messengerService)
{
    [RelayCommand]
    private Task GoToHome()
    {
        navigationService.NavigateToHome();
        return Task.CompletedTask;
    }
}