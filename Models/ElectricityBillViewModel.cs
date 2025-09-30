using System;

namespace WebBilling_Lahore_ReactCore.Models
{
    public class ElectricityBillViewModel
    {
        public string InvoiceNo { get; set; }
        public string CustomerNo { get; set; }
        public string CustomerName { get; set; }
        public string BTNo { get; set; }
        public string BillingMonth { get; set; }
        public string BillingYear { get; set; }
        public DateTime? AmountDueDate { get; set; }      // Maps to [DueDate] in DB
        public decimal? AmountAfterDate { get; set; }     // Maps to [BillAmountAfterDueDate] in DB
    }
}
