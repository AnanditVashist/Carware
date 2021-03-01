using Carware.Models;
using Carware.ViewModels;
using System.Threading.Tasks;

namespace Carware.Interface
{
    public interface ICarService
    {
        void SaveCarInDb(CarViewModel viewModel);
        Task<CarViewModel> TurnCarToViewModelAsync(Car car);
        Car TurnViewModelToCar(CarViewModel car);
        string GetCurrentLoggedInUserId();
        Task<CarViewModel> GetEditCarViewModel(int id);
        void UpdateCarInDb(CarViewModel viewModel);
        Task<InventoryViewModel> GetInventory();

        Task<SellCarViewModel> GetSellCarViewModel(int carId);
        void SellCar(SellCarViewModel viewModel);
        Task<InventoryViewModel> SoldVehicles();
    }
}
