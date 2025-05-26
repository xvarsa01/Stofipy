using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace Stofipy.AppDotVVM.ViewModels;

public abstract partial class ViewModelBase : ObservableRecipient, IViewModel, INotifyPropertyChanged
{
    private bool _forceDataRefresh = true;

    protected ViewModelBase()
        : base()
    {
        //MessengerService = messengerService;
        IsActive = true;
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
