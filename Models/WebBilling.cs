using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBilling_Lahore_ReactCore.Models
{
    [Table("EBillingNotes")]
    public class EBillingNote
    {
        [Key]
        public int ID { get; set; }

        public int? uid { get; set; }
        public string paymentstatus { get; set; }
        public string REFRENCENOBARCODE { get; set; }
        public string Customer_Name { get; set; }
        public string Plot_Number { get; set; }
        public string Meter_Number { get; set; }
        public string GTotalAmount { get; set; }
        public string totalunit { get; set; }
        public string TotalElecCharges { get; set; }
        public string GTotal { get; set; }
        public string InvoiceNo { get; set; }

        // ✅ Added missing fields used in controller
        public string billing_month { get; set; }
        public string billing_year { get; set; }
        public string billtotal { get; set; }
        public string amountduedate { get; set; }
        public string amountafterdate { get; set; }
        public string total_units { get; set; }
        public string Sector { get; set; }
        public string Block { get; set; }

    }
}
