using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBilling_Lahore_ReactCore.Models;

namespace WebBilling_Lahore_ReactCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Electricity_NetMeterBillController : ControllerBase
    {
        private readonly SSQReactCoreContext _context;

        private static readonly List<string> MonthOrder = new()
        {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };

        public Electricity_NetMeterBillController(SSQReactCoreContext context)
        {
            _context = context;
        }

        // Single entry point: pehle NetMeter, na mile to Normal ElectricityBill.
        // Kahin data na ho to 404 ki jagah 200 OK + { found = false } return hota hai,
        // taake browser console par koi 404 error na aaye aur frontend sirf 1 hi API call kare.
        [HttpGet]
        public async Task<IActionResult> GetBill(string BTNo, string Project)
        {
            try
            {
                var netMeterResult = await BuildNetMeterResult(BTNo, Project);
                if (netMeterResult != null)
                {
                    return Ok(new { found = true, billType = "NetMeter", data = netMeterResult });
                }

                var normalResult = await BuildNormalResult(BTNo, Project);
                if (normalResult != null)
                {
                    return Ok(new { found = true, billType = "Normal", data = normalResult });
                }

                return Ok(new { found = false, billType = (string?)null, data = (object?)null });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving data: " + ex.Message);
            }
        }

        private async Task<object?> BuildNetMeterResult(string BTNo, string Project)
        {
            var latestData = (from bill in _context.ElectricityBillsNetMeter
                              join cust in _context.CustomersDetail
                              on bill.BTNo equals cust.BTNo
                              where bill.BTNo == BTNo && cust.Project == Project
                              select new
                              {
                                  ElectricityBillsNetMeter = bill,
                                  CustomerDetail = cust
                              })
                               .AsEnumerable()
                               .OrderByDescending(x => Convert.ToInt32(x.ElectricityBillsNetMeter.BillingYear))
                               .ThenByDescending(x => MonthOrder.IndexOf(x.ElectricityBillsNetMeter.BillingMonth))
                               .FirstOrDefault();

            if (latestData == null)
                return null;

            int latestYear = Convert.ToInt32(latestData.ElectricityBillsNetMeter.BillingYear);
            string latestMonth = latestData.ElectricityBillsNetMeter.BillingMonth;
            int latestMonthIndex = MonthOrder.IndexOf(latestMonth);

            var bills = await _context.ElectricityBillsNetMeter
                .Where(b => b.BTNo == BTNo &&
                            (Convert.ToInt32(b.BillingYear) == latestYear ||
                             Convert.ToInt32(b.BillingYear) == latestYear - 1))
                .Select(b => new
                {
                    BillingMonth = b.BillingMonth,
                    BillingYear = Convert.ToInt32(b.BillingYear),
                    Units = (decimal?)(b.SumUnitsImport ?? 0),
                    Bill = (b.BillAmountInDueDate == null || b.BillAmountInDueDate == 0)
                        ? (decimal?)(b.BillAmount ?? 0)
                        : (decimal?)(b.BillAmountInDueDate ?? 0),
                })
                .ToListAsync();

            var latestYearData = bills
                .Where(x => x.BillingYear == latestYear)
                .OrderBy(x => MonthOrder.IndexOf(x.BillingMonth))
                .ToList();

            var previousYearData = bills
                .Where(x => x.BillingYear == latestYear - 1)
                .OrderBy(x => MonthOrder.IndexOf(x.BillingMonth))
                .ToList();

            var finalMonths = MonthOrder.Take(latestMonthIndex + 1)
                .Select(m => latestYearData.FirstOrDefault(x => x.BillingMonth == m) ??
                             new { BillingMonth = m, BillingYear = latestYear, Units = (decimal?)0, Bill = (decimal?)0 })
                .ToList();

            var remainingMonths = MonthOrder.Skip(latestMonthIndex + 1)
                .Select(m => previousYearData.FirstOrDefault(x => x.BillingMonth == m) ??
                             new { BillingMonth = m, BillingYear = latestYear - 1, Units = (decimal?)0, Bill = (decimal?)0 })
                .ToList();

            var fullYearData = finalMonths.Concat(remainingMonths).ToList();

            return new
            {
                latestData.ElectricityBillsNetMeter,
                latestData.CustomerDetail,
                BillHistory = fullYearData
            };
        }

        private async Task<object?> BuildNormalResult(string BTNo, string Project)
        {
            var latestData = (from bill in _context.ElectricityBills
                              join cust in _context.CustomersDetail
                              on bill.BTNo equals cust.BTNo
                              where bill.BTNo == BTNo && cust.Project == Project
                              select new
                              {
                                  ElectricityBill = bill,
                                  CustomerDetail = cust
                              })
                                .AsEnumerable()
                                .OrderByDescending(x => Convert.ToInt32(x.ElectricityBill.BillingYear))
                                .ThenByDescending(x => MonthOrder.IndexOf(x.ElectricityBill.BillingMonth))
                                .FirstOrDefault();

            if (latestData == null)
                return null;

            int latestYear = Convert.ToInt32(latestData.ElectricityBill.BillingYear);
            string latestMonth = latestData.ElectricityBill.BillingMonth;
            int latestMonthIndex = MonthOrder.IndexOf(latestMonth);

            var bills = await _context.ElectricityBills
                .Where(b => b.BTNo == BTNo &&
                            (Convert.ToInt32(b.BillingYear) == latestYear ||
                             Convert.ToInt32(b.BillingYear) == latestYear - 1))
                .Select(b => new
                {
                    BillingMonth = b.BillingMonth,
                    BillingYear = Convert.ToInt32(b.BillingYear),
                    Units = b.TotalUnit ?? 0,
                    Bill = b.BillAmountInDueDate ?? 0m,
                    Payment =
                        b.PaymentStatus == "Paid"
                        ? (b.BillAmountInDueDate ?? 0).ToString()
                        : b.PaymentStatus == "Paid With Surcharge"
                        ? (b.BillAmountAfterDueDate ?? 0).ToString()
                        : b.PaymentStatus == "Partially Paid"
                        ? "Partially Paid"
                        : "0"
                })
                .ToListAsync();

            var latestYearData = bills
                .Where(x => x.BillingYear == latestYear)
                .OrderBy(x => MonthOrder.IndexOf(x.BillingMonth))
                .ToList();

            var previousYearData = bills
                .Where(x => x.BillingYear == latestYear - 1)
                .OrderBy(x => MonthOrder.IndexOf(x.BillingMonth))
                .ToList();

            var finalMonths = MonthOrder.Take(latestMonthIndex + 1)
                .Select(m => latestYearData.FirstOrDefault(x => x.BillingMonth == m) ??
                             new { BillingMonth = m, BillingYear = latestYear, Units = 0m, Bill = 0m, Payment = "0" })
                .ToList();

            var remainingMonths = MonthOrder.Skip(latestMonthIndex + 1)
                .Select(m => previousYearData.FirstOrDefault(x => x.BillingMonth == m) ??
                             new { BillingMonth = m, BillingYear = latestYear - 1, Units = 0m, Bill = 0m, Payment = "0" })
                .ToList();

            var fullYearData = finalMonths.Concat(remainingMonths).ToList();

            return new
            {
                latestData.ElectricityBill,
                latestData.CustomerDetail,
                BillHistory = fullYearData
            };
        }
    }
}
