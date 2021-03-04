using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Data {
    public class SeedingService {

        private SalesWebMvcContext _context;

        public SeedingService(SalesWebMvcContext context) {
            _context = context;
        }

        public void Seed() {

            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any()) return; //DB already has been seeded;

            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Eletronics");
            Department d3 = new Department(3, "Games");
            Department d4 = new Department(4, "Books");
            Department d5 = new Department(5, "Fashion");

            Seller s1 = new Seller(1, "Lucas H", "lucas@hotmail.com",new DateTime(1999,1,1),2000.0,d1);
            Seller s2 = new Seller(2, "Ludwick", "lud@hotmail.com", new DateTime(1999, 2, 2), 5000.0, d2);
            Seller s3 = new Seller(3, "Henrico", "henrico@hotmail.com", new DateTime(1999, 3, 3), 10000.0, d5);

            SalesRecord r1 = new SalesRecord(1, new DateTime(2020, 1, 14), 5850.0, SaleStatus.Pending, s1);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2020, 2, 2), 5850.0, SaleStatus.Billed, s2);
            SalesRecord r3 = new SalesRecord(3, new DateTime(2020, 3, 22), 5850.0, SaleStatus.Pending, s2);
            SalesRecord r4 = new SalesRecord(4, new DateTime(2020, 4, 4), 5850.0, SaleStatus.Canceled, s3);

            _context.Department.AddRange(d1,d2,d3,d4,d5);
            _context.Seller.AddRange(s1,s2,s3);
            _context.SalesRecord.AddRange(r1,r2,r3,r4);

            _context.SaveChanges();
        }
    }
}
