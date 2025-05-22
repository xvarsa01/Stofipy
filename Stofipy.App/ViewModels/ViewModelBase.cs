using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Stofipy.BL.Models;

namespace Stofipy.App.ViewModels;

public abstract partial class ViewModelBase : ObservableObject, IViewModel, INotifyPropertyChanged
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
    
    
    [ObservableProperty]
    private string _shuffleButton = "shuffle_white.png";
    private bool _shuffle = false;
    
    [RelayCommand]
    private Task ToggleShuffle(FileListModel item)
    {
        if (_shuffle)
        {
            _shuffle = false;
            ShuffleButton = "shuffle_white.png";
        }
        else
        {
            _shuffle = true;
            ShuffleButton = "shuffle_green.png";
        }
        return Task.CompletedTask;
    }
}
