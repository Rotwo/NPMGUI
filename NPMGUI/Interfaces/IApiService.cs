using System.Threading.Tasks;
using NPMGUI.DTOs;

namespace NPMGUI.Interfaces;

public interface IApiService
{
    Task<ApiPackageSearchResult?> SearchPackageAsync(string query);
}