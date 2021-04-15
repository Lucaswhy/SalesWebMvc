using SalesWebMvc.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models {
    public class Order {
        public int Id { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public double Price { get; set; }
        public int Instalment { get; set; }
        public int Quantity { get; set; }
        public List<Product> ListProduct { get; set; }
        
        public Order() {

        }

        public Order(int id, double price, int instalment, int quantity) {
            Id = id;
            Price = price;
            Instalment = instalment;
            Quantity = quantity;
        }

        public void Add(Product p) {
            ListProduct.Add(p);
        }
        public void Delete(Product p) {
            ListProduct.Remove(p);
        }

        public List<double> ListInstalments() {

            List<double> instalList = new List<double>();
            double iValue = 0;

            for (int i = 1; i <= Instalment; i++) {

                iValue = (Price / i);
                instalList.Add(iValue);
            }

            return instalList;
        }
    }
}
