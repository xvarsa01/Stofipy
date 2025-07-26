using Stofipy.BL.Models;

namespace Stofipy.BL.Facades.Interfaces;

public interface IGlobalFacade
{
    Task<GlobalSearchModel> SearchGloballyAsync(string searchTerm);
}