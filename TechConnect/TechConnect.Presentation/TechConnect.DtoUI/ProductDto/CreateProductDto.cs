using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.DtoUI.ProductDto
{
    public class CreateProductDto
    {
        public CreateProductDto()
        {
            TechnicalSpecifications = new Dictionary<string, string>();
        }

        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
        public int Rate { get; set; }
        public string CategoryId { get; set; }
        public string ColorId { get; set; }


        public string ProductDescription { get; set; }
        public string ProductInformation { get; set; }
        public string MaterialUsed { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }
        public string VideoUrl4 { get; set; }
        public decimal StockCode { get; set; }
        public string Brand { get; set; }
        public int StockQuantity { get; set; }
        public string WarrantyInfo { get; set; }
        public Dictionary<string, string> TechnicalSpecifications { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsFeatured { get; set; } //girişteki 3 lü yer için
        public bool IsFavourite { get; set; }
        public bool IsCompare { get; set; }
    }
}
