using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBilling_Lahore_ReactCore.Models
{
    [Table("ElectricityBills")] // maps class to table
    public class ElectricityBill
    {
        [Key] // ✅ Primary key
        public int uid { get; set; }

        public string InvoiceNo { get; set; }
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string BTNo { get; set; }
        public string BillingMonth { get; set; }
        public string BillingYear { get; set; }

        public DateTime? DueDate { get; set; }
        public DateTime? BillingDate { get; set; }
        public DateTime? ReadingDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ValidDate { get; set; }

        public int? BillAmountAfterDueDate { get; set; }
    }
}
