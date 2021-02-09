namespace Carware.ViewModels
{
    public class SellCarViewModel
    {

        public CarViewModel Car { get; set; }
        public CustomerViewModel CustomerDetails { get; set; }
        public string SellingPrice { get; set; }
        public string DiscountGiven { get; set; }
        public string SellerId { get; set; }
    }
}
