using Microsoft.EntityFrameworkCore;
using WebBilling.Models; // ✅ Import your EBillingNote model namespace
using WebBilling_Lahore_ReactCore.Models;

namespace WebBilling_Lahore_ReactCore.Models
{
    public class SSQReactCoreContext : DbContext
    {
        public SSQReactCoreContext(DbContextOptions<SSQReactCoreContext> options)
            : base(options)
        {
        }
        public DbSet<ElectricityBill> ElectricityBills { get; set; } // ✅ maps to DB table
                                                                     // public DbSet<CustomersDetail> CustomersDetails { get; set; }
        public DbSet<CustomersDetail> CustomersDetails { get; set; }

    }
}
