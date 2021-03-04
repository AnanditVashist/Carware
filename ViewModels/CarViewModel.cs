using Carware.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Carware.ViewModels
{
    public class CarViewModel
    {
        public CarViewModel()
        {
            PhotoString = new List<string>();
            Photos = new List<IFormFile>();
        }
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PurchasePrice { get; set; }
        public string IdealSellingPrice { get; set; }
        public string SellingPrice { get; set; }
        public string MaxDiscount { get; set; }
        public DateTime SellDate { get; set; }
        public ApplicationUser Seller { get; set; }
        public List<String> PhotoString { get; set; }
        public List<IFormFile> Photos { get; set; }

    }

}
