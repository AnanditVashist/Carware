using System;

namespace Carware.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime Year { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? SellingDate { get; set; }
        public int PurchasePrice { get; set; }
        public int? SellingPrice { get; set; }
        public int MaxDiscount { get; set; }
        public Customer Customer { get; set; }
        public int? CustomerId { get; set; }
        public ApplicationUser Seller { get; set; }
    }
}
