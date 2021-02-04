using System.Collections.Generic;

namespace Carware.ViewModels
{
    public class InventoryViewModel
    {
        public List<CarViewModel> Inventory { get; set; }
        public InventoryViewModel()
        {
            Inventory = new List<CarViewModel>();
        }
    }
}
