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
        public string? Status { get; set; }
        public string? CreationDate { get; set; }
        public string? inheritedphase { get; set; }
        public string? inheritedplot { get; set; }
        public string? ct_meter { get; set; }
        public string? installmentarrears { get; set; }
        public string? FEEDER { get; set; }
        public string? PaymentTypeSetByBarCode { get; set; }
        public string? FirstUnitsRate { get; set; }
        public string? previousLog { get; set; }
        public string? presentLog { get; set; }
        public string? totalunitLog { get; set; }
        public string? amountduedateLog { get; set; }
        public string? amountafterdateLog { get; set; }
        public string? PaidOnSetByBarCode { get; set; }
        public string? BillCreationOperator { get; set; }
        public string? present_Log { get; set; }
        public string? billing_month_Log { get; set; }
        public string? FPA1 { get; set; }
        public string? total_units_Log { get; set; }
        public string? billing_year_Log { get; set; }
        public string? FPA2 { get; set; }
        public string? subsidy_Log { get; set; }
        public string? Reading_PHCurrent_Log { get; set; }
        public string? FPA3 { get; set; }
        public string? paymentstatus_Log { get; set; }
        public string? Reading_OffPHCurrent_Log { get; set; }
        public string? partiallypaid_Log { get; set; }
        public string? AddExtraTax { get; set; }
        public string? partiallypaid2_Log { get; set; }
        public string? date_reading { get; set; }
        public string? date_issue { get; set; }
        public string? date_due { get; set; }
        public string? present { get; set; }
        public string? first_units { get; set; }
        public string? second_units { get; set; }
        public string? third_units { get; set; }
        public string? fourth_units { get; set; }
        public string? Reading_PHCurrent { get; set; }
        public string? Reading_OffPHCurrent { get; set; }
        public string? UnitsDiff_PH { get; set; }
        public string? UnitsDiff_OffPH { get; set; }
        public string? MDIFactor { get; set; }
        public string? TotalSubsidy { get; set; }
        public string? installmentno { get; set; }
        public string? amountduedate { get; set; }
        public string? surcharge { get; set; }
        public string? amountafterdate { get; set; }
        public string? bankbranch { get; set; }
        public string? paymentstatus { get; set; }
        public string? InstallmentPaidType { get; set; }
        public string? paymentremaining { get; set; }
        public string? partiallypaid2 { get; set; }
        public string? paymentremaining2 { get; set; }
        public string? commt { get; set; }
        public string? recoverywithsurcharge { get; set; }
        public string? txtunpaid { get; set; }
        public string? keyreading { get; set; }
        public string? txtlastmonthstatus { get; set; }
        public string? txtlastmonthamount { get; set; }
        public string? txtlastmonthafterduedate { get; set; }
        public string? txtarrears { get; set; }
        public string? amounthavepaid { get; set; }
        public string? saveallow { get; set; }
        public string? Rep_recovery { get; set; }
        public string? Rep_recoverywithsurcharge { get; set; }
        public string? Rep_outstandingwithsurcharge { get; set; }
        public string? Unpaid_Months { get; set; }
        public string? readyforprint { get; set; }
        public string? totalpayment4adv { get; set; }
        public string? NewCustomer_Refrence { get; set; }
        public string? Meter_Block_Plot { get; set; }
        public string? REFRENCENOBARCODE { get; set; }
        public string? Meter_Type { get; set; }
        public string? Meter_Number_1 { get; set; }
        public string? remgeneration { get; set; }
        public string? Customer_Name { get; set; }
        public string? Plot_Number { get; set; }
        public string? Meter_Number { get; set; }
        public string? Sector { get; set; }
        public string? installedon { get; set; }
        public string? Block { get; set; }
        public string? Plot_Size { get; set; }
        public string? billing_month { get; set; }
        public string? Plot_Category { get; set; }
        public string? Plot_Status { get; set; }
        public string? Tariff { get; set; }
        public string? connectionstatus { get; set; }
        public string? billing_year { get; set; }
        public string? IsTwoReadingMeter { get; set; }
        public string? NIC_Number { get; set; }
        public string? IncomeTaxWithHeld_Flag { get; set; }
        public string? NTN_Number { get; set; }
        public string? SalesTaxForRetailer { get; set; }
        public string? FurtherTax_Flag { get; set; }
        public string? previous { get; set; }
        public string? ct_diff { get; set; }
        public string? ct_mf { get; set; }
        public string? display_first_units { get; set; }
        public string? GTotal { get; set; }
        public string? FPA_Comment { get; set; }
        public string? BillType { get; set; }
        public string? GeneratorCharges { get; set; }
        public string? BillMeteringType { get; set; }
        public string? MiscElectCharges { get; set; }
        public string? OPCRate { get; set; }
        public string? SumAmount { get; set; }
        public string? UnitRate { get; set; }
        public string? ValidDate { get; set; }
        public string? InvoiceNo { get; set; }
        public string? IncomeTaxCommercial { get; set; }
        public string? OPCAmount { get; set; }
        public string? FPA { get; set; }

        // 🔹 Month-wise fields
        public string? jan_year { get; set; }
        public string? jan_units { get; set; }
        public string? jan { get; set; }
        public string? jan_status { get; set; }

        public string? feb_year { get; set; }
        public string? feb_units { get; set; }
        public string? feb { get; set; }
        public string? feb_status { get; set; }

        public string? mar_year { get; set; }
        public string? mar_units { get; set; }
        public string? mar { get; set; }
        public string? mar_status { get; set; }

        public string? april_year { get; set; }
        public string? april_units { get; set; }
        public string? april { get; set; }
        public string? april_status { get; set; }

        public string? may_year { get; set; }
        public string? may_units { get; set; }
        public string? may { get; set; }
        public string? may_status { get; set; }

        public string? june_year { get; set; }
        public string? june_units { get; set; }
        public string? june { get; set; }
        public string? june_status { get; set; }

        public string? july_year { get; set; }
        public string? july_units { get; set; }
        public string? july { get; set; }
        public string? july_status { get; set; }

        public string? aug_year { get; set; }
        public string? aug_units { get; set; }
        public string? aug { get; set; }
        public string? aug_status { get; set; }

        public string? sep_year { get; set; }
        public string? sep_units { get; set; }
        public string? sep { get; set; }
        public string? sep_status { get; set; }

        public string? oct_year { get; set; }
        public string? oct_units { get; set; }
        public string? oct { get; set; }
        public string? oct_status { get; set; }

        public string? nov_year { get; set; }
        public string? nov_units { get; set; }
        public string? nov { get; set; }
        public string? nov_status { get; set; }

        public string? dec_year { get; set; }
        public string? dec_units { get; set; }
        public string? dec { get; set; }
        public string? dec_status { get; set; }
    }
}
