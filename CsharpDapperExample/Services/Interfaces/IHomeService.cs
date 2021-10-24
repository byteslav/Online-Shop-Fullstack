using System.Threading.Tasks;
using CsharpDapperExample.ViewModels;

namespace CsharpDapperExample.Services.Interfaces
{
    public interface IHomeService
    {
        Task<HomeViewModel> GetHomeViewModelAsync();
        Task<DetailsViewModel> GetDetailsViewModelAsync(int id);
        void AddToCart(int id);
        void RemoveFromCart(int id);
    }
}