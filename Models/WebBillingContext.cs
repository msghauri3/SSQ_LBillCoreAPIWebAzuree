using Microsoft.EntityFrameworkCore;
using WebBilling_Lahore_ReactCore.Models;

namespace WebBilling_Lahore_ReactCore.Models
{
    public class WebBillingContext : DbContext
    {
        public WebBillingContext(DbContextOptions<WebBillingContext> options)
            : base(options)
        {
        }

        // ✅ Use your main model class defined in Models/EBillingNote.cs
        public DbSet<EBillingNote> EBillingNotes { get; set; }
        public DbSet<ElectricityBill> ElectricityBills { get; set; } // ✅ maps to DB table
    }
}
