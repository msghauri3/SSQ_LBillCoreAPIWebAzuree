using System.ComponentModel.DataAnnotations;

namespace WebBilling_Lahore_ReactCore.Models
{
    public class CustomersDetail
    {
        [Key]
        public int Uid { get; set; }

        [Required]
        public string CustomerNo { get; set; }
        public string? BTNo { get; set; }
        public string? CustomerName { get; set; }
        public string? GeneratedMonthYear { get; set; }
        public string? LocationSeqNo { get; set; }
        public string? CNICNo { get; set; }
        public string? FatherName { get; set; }
        public string? InstalledOn { get; set; }
        public string? MobileNo { get; set; }
        public string? TelephoneNo { get; set; }
        public string? MeterType { get; set; }
        public string? NTNNumber { get; set; }
        public string? City { get; set; }
        public string Project { get; set; }
        public string SubProject { get; set; }
        public string TariffName { get; set; }
        public string? BankNo { get; set; }
        public string? BTNoMaintenance { get; set; }
        public string Category { get; set; }
        public string Block { get; set; }
        public string? PlotType { get; set; }
        public string? Size { get; set; }
        public string Sector { get; set; }
        public string PloNo { get; set; }
        public string? BillStatusMaint { get; set; }
        public string? BillStatus { get; set; }
        public string? History { get; set; }
        public string? MeterNo { get; set; }
        public string? BillGenerationStatus { get; set; }
    }
}
