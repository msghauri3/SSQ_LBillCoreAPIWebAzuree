using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBilling_Lahore_ReactCore.Models
{
    [Table("MaintenanceBills")]
    public class MaintenanceBills
    {
        [Key]
        public int uid { get; set; }

        public string? InvoiceNo { get; set; }
        public string? CustomerNo { get; set; }
        public string? CustomerName { get; set; }
        public string? PlotStatus { get; set; }
        public string? MeterNo { get; set; }
        public string? BTNo { get; set; }
        public string? BillingMonth { get; set; }
        public string? BillingYear { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BillingDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? IssueDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ValidDate { get; set; }

        public string? PaymentStatus { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PaymentDate { get; set; }

        public string? PaymentMethod { get; set; }
        public string? BankDetail { get; set; }

        public DateTime? LastUpdated { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal? MaintCharges { get; set; }

        public int? BillAmountInDueDate { get; set; }
        public int? BillSurcharge { get; set; }
        public int? BillAmountAfterDueDate { get; set; }

        [Column(TypeName = "decimal(18,1)")]
        public decimal? Arrears { get; set; }

        public string? History { get; set; }

        public int? TaxAmount { get; set; }
        public int? Fine { get; set; }
        public int? OtherCharges { get; set; }
        public int? WaterCharges { get; set; }
    }
}
