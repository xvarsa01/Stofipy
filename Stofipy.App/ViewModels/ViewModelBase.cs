using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Stofipy.App.ViewModels;

public abstract class ViewModelBase : IViewModel, INotifyPropertyChanged
{
    private bool _forceDataRefresh = true;

    // protected readonly IMessengerService MessengerService;

    // protected ViewModelBase(IMessengerService messengerService)
    //     : base(messengerService.Messenger)
    protected ViewModelBase()
        : base()
    {
        // MessengerService = messengerService;
        // IsActive = true;
    }

    public async Task OnAppearingAsync()
    {
        if (_forceDataRefresh)
        {
            await LoadDataAsync();

            _forceDataRefresh = false;
        }
    }

    protected virtual Task LoadDataAsync()
        => Task.CompletedTask;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
