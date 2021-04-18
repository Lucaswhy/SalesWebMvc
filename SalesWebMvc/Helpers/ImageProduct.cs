using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using SalesWebMvc.Models;

namespace SalesWebMvc.Helpers {
    public class ImageProduct : Product {
        public IFormFile Content { get; set; }

        public ImageProduct() {
        }

        public ImageProduct(int id, string sku, string name, double price, string description,int instalments, int stock, string image, bool active) {
            Id = id;
            SKU = sku;
            Name = name;
            Price = price;
            Description = description;
            Instalments = instalments;
            Stock = stock;
            Image = image;
            Active = active;
        }

    }
}
