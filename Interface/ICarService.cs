using Carware.Models;
using Carware.ViewModels;

namespace Carware.Interface
{
    public interface ICarService
    {
        void SaveCarInDb(CarViewModel viewModel);
        CarViewModel TurnCarToViewModel(Car car);
        Car TurnViewModelToCar(CarViewModel car);
        string GetCurrentLoggedInUserId();
        CarViewModel GetEditCarViewModel(int id);
        void UpdateCarInDb(CarViewModel viewModel);
        InventoryViewModel GetInventory();
    }
}
