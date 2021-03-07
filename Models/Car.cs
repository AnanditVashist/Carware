using System;
using System.Collections.Generic;

namespace Carware.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? SellingDate { get; set; }
        public int PurchasePrice { get; set; }
        public int IdealSellingPrice { get; set; }
        public int? ActualSellingPrice { get; set; }
        public int MaxDiscount { get; set; }
        public Customer Customer { get; set; }
        public int? CustomerId { get; set; }
        public ApplicationUser Seller { get; set; }
        public string SellerId { get; set; }
        public List<Photo> Photos { get; set; }

        public string Mileage { get; set; }

    }
}
