using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models {
    public class Product {
        [Required(ErrorMessage = "{0} Required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} Required.")]
        public String SKU { get; set; }
        public String Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Price { get; set; }
        public String Description { get; set; }
        public int Instalments { get; set; }
        public int Stock { get; set; }
        public byte[] Content { get; set; }
        public bool Active { get; set; }

        public Product() {

        }

        public Product(int id, String sku, string name, double price, string description, int instalments) {
            Id = id;
            SKU = sku;
            Name = name;
            Price = price;
            Description = description;
            Instalments = instalments;
            Stock = 0;
            Active = true;
        }

        public List<double> ListInstalments() {

            List<double> instalList = new List<double>();
            double iValue = 0;

            for (int i=1;i<=Instalments;i++) {

                iValue = (Price / i);
                instalList.Add(iValue);
            }

            return instalList;
        }
    }
}
