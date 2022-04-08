using System;
namespace Cards.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string BarCode { get; set; }
        public string Image { get; set; }

        public string Status { get; set; }
        public string Description { get; set; }

        public Product() {}

        public Product(int id, string name, string barcode, string img,
           string status, string des)
        {
            ID = id;
            Name = name;
            BarCode = barcode;
            Image = img;
            Status = status;
            Description = des;
        }
    }
}
