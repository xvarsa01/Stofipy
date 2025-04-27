using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Stofipy.BL.Models.InterfaceBase;

public abstract record ModelBase : INotifyPropertyChanged, IModel
{
    [Required(ErrorMessage = "Id is required")]
    public required Guid Id { get; init; }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

}