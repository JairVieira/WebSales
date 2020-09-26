using SalesWeb.Models;
using SalesWeb.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWeb.Data
{
    public class SeedingService
    {
        private SalesWebContext _context;

        public SeedingService( SalesWebContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Department.Any() ||
                _context.Seller.Any() ||
                _context.SalesRecord.Any())
                return;

            var d1 = new Department(1, "Computers");
            var d2 = new Department(2, "Electronics");
            var d3 = new Department(3, "Fashion");
            var d4 = new Department(4, "Books");

            var s1 = new Seller(1, "Bob Brad", "bob@bob.com", new DateTime(1990, 1, 2),2000, d1);
            var s2 = new Seller(2, "John Kra", "bob@bob.com", new DateTime(1991, 4, 2),3000, d2);
            var s3 = new Seller(3, "Mad Bro", "bob@bob.com", new DateTime(1980, 5, 3),2000, d3);
            var s4 = new Seller(4, "Jane B", "bob@bob.com", new DateTime(1970, 2, 12),5000, d4);
            var s5 = new Seller(5, "Mary Brad", "bob@bob.com", new DateTime(1985, 6, 16),6000, d4);
            var s6 = new Seller(6, "Jim Lu", "bob@bob.com", new DateTime(1990, 1, 27),9000, d2);

            var r1 = new SalesRecord(1, new DateTime(2020, 08, 12), 15000, SaleStatus.Billed, s6);
            var r2 = new SalesRecord(2, new DateTime(2020, 08, 12), 1000, SaleStatus.Billed, s5);
            var r3 = new SalesRecord(3, new DateTime(2020, 08, 12), 5000, SaleStatus.Billed, s4);
            var r4 = new SalesRecord(4, new DateTime(2020, 08, 12), 1500, SaleStatus.Billed, s3);
            var r5 = new SalesRecord(5, new DateTime(2020, 08, 12), 12000, SaleStatus.Billed, s2);
            var r6 = new SalesRecord(6, new DateTime(2020, 08, 12), 18000, SaleStatus.Billed, s1);
            var r7 = new SalesRecord(7, new DateTime(2020, 08, 12), 15000, SaleStatus.Billed, s6);
            var r8 = new SalesRecord(8, new DateTime(2020, 08, 12), 17000, SaleStatus.Billed, s6);
            var r9 = new SalesRecord(9, new DateTime(2020, 08, 12), 114500, SaleStatus.Billed, s5); 
            var r10 = new SalesRecord(10, new DateTime(2020, 08, 12), 15547000, SaleStatus.Billed, s3);
            var r11= new SalesRecord(11, new DateTime(2020, 08, 12), 1545000, SaleStatus.Billed, s1);

            _context.Department.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(s1, s2, s3, s4, s5, s6);
            _context.SalesRecord.AddRange(r1, r2, r3, r4, r5, r6, r7, r7, r8, r8, r9, r10, r11);

            _context.SaveChanges();
        }
    }
}
