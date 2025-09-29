using Microsoft.EntityFrameworkCore;
using WebBilling.Models; // ✅ Import your EBillingNote model namespace

namespace WebBilling_Lahore_ReactCore.Models
{
    public class WebBillingDbContext : DbContext
    {
        public WebBillingDbContext(DbContextOptions<WebBillingDbContext> options)
            : base(options)
        {
        }

        // ✅ Use your main model class defined in Models/EBillingNote.cs
        public DbSet<EBillingNote> EBillingNotes { get; set; }
    }
}
