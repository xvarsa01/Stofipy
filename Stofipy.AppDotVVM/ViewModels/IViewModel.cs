using System.Threading.Tasks;

namespace Stofipy.AppDotVVM.ViewModels;

public interface IViewModel
{
    Task OnAppearingAsync();
}