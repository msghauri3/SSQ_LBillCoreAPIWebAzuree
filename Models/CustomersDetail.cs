using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBilling_Lahore_ReactCore.Models
{
    [Table("CustomersDetail")]
    public class CustomersDetail
    {
        [Key]
        public int uid { get; set; }
        public string? CustomerNo { get; set; }
        public string? BTNo { get; set; }
        public string? CustomerName { get; set; }
        public string? Sector { get; set; }
        public string? Block { get; set; }
        public string? Project { get; set; }
        public string? BillingType { get; set; } // optional if you want to map TariffName or Category
    }
}
