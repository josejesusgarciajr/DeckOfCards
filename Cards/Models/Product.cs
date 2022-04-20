using System;
using Microsoft.AspNetCore.Http;

namespace Cards.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string BarCode { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }

        public string Status { get; set; }
        public string Description { get; set; }

        // for image upload
        public IFormFile FormFile { get; set; }

        public Product() {}

        public Product(int id, string name, string barcode, string img,
            string category, string status, string des)
        {
            ID = id;
            Name = name;
            BarCode = barcode;
            Image = img;
            Category = category;
            Status = status;
            Description = des;
        }
    }
}
