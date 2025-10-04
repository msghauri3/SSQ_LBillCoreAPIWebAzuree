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
        public DbSet<ElectricityBill> ElectricityBills { get; set; }

    }
}
