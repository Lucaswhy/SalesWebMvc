using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models {
    public class Seller {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} Required.")]
        [StringLength(100,MinimumLength = 2,ErrorMessage = "Please, type your name correctly.")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Enter a valid {0}")]
        [Required(ErrorMessage = "{0} Required.")]
        public string Email { get; set; }
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }

        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();


        public Seller() {

        }

        public Seller(int id, string name, string email, DateTime birthdate, double baseSalary, Department department) {
            Id = id;
            Name = name;
            Email = email;
            Birthdate = birthdate;
            BaseSalary = baseSalary;
            Department = department;
        }


        public void addSales(SalesRecord sr) {
            Sales.Add(sr);
        }

        public void removeSales(SalesRecord sr) {
            Sales.Remove(sr);
        }

        public double totalSales(DateTime initial, DateTime final) {
            return Sales.Where(s => s.Date >= initial && s.Date <= final).Sum(s => s.Amount);
        }

    }
}
