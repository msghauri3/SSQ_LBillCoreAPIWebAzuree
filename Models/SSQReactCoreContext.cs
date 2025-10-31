using Microsoft.EntityFrameworkCore;

namespace WebBilling_Lahore_ReactCore.Models
{
    public class SSQReactCoreContext : DbContext
    {
        public SSQReactCoreContext(DbContextOptions<SSQReactCoreContext> options)
            : base(options)
        {
        }

        public DbSet<CustomersDetail> CustomersDetail { get; set; }
        public DbSet<ElectricityBills> ElectricityBills { get; set; }
        public DbSet<MaintenanceBills> MaintenanceBills { get; set; }
        public DbSet<CustomersMaintenance> CustomersMaintenance { get; set; }
        public DbSet<ElectricityBillsNetMeter> ElectricityBillsNetMeter { get; set; }

    }
}
