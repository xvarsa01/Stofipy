using System.ComponentModel.DataAnnotations;

namespace Stofipy.BL.Models.InterfaceBase;

public abstract record ModelBase : IModel
{
    [Required(ErrorMessage = "Id is required")]
    public required Guid Id { get; init; }

}