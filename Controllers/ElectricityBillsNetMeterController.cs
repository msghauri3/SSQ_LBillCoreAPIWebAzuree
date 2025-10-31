using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebBilling_Lahore_ReactCore.Models;

namespace WebBilling_Lahore_ReactCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityBillsNetMeterController : ControllerBase
    {
        private readonly SSQReactCoreContext _context;
        // ✅ Month order defined once, reused everywhere
        private static readonly List<string> MonthOrder = new()
        {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };

        public ElectricityBillsNetMeterController(SSQReactCoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetElectricityBillsNetMeter(string BTNo, string Project)
        {
            try
            {
                // Step 1: Get latest bill with customer details
                var latestData = (from bill in _context.ElectricityBillsNetMeter
                                  join cust in _context.CustomersDetail
                                  on bill.BTNo equals cust.BTNo
                                  where bill.BTNo == BTNo && cust.Project == Project
                                  select new
                                  {
                                      ElectricityBillsNetMeter = bill,
                                      CustomerDetail = cust
                                  })
                                   .AsEnumerable() // <-- Force client-side sorting
                                   .OrderByDescending(x => Convert.ToInt32(x.ElectricityBillsNetMeter.BillingYear))
                                   .ThenByDescending(x => MonthOrder.IndexOf(x.ElectricityBillsNetMeter.BillingMonth))
                                   .FirstOrDefault();

                if (latestData == null)
                    return NotFound("No record found for given BTNo and Project.");

                // Step 2: Get the latest bill year and month
                int latestYear = Convert.ToInt32(latestData.ElectricityBillsNetMeter.BillingYear);
                string latestMonth = latestData.ElectricityBillsNetMeter.BillingMonth;

                // Step 3: Month order
                int latestMonthIndex = MonthOrder.IndexOf(latestMonth);

                // Step 4: Fetch both years' data
                var bills = await _context.ElectricityBillsNetMeter
                    .Where(b => b.BTNo == BTNo &&
                                (Convert.ToInt32(b.BillingYear) == latestYear ||
                                 Convert.ToInt32(b.BillingYear) == latestYear - 1))
                    .Select(b => new
                    {
                        BillingMonth = b.BillingMonth,
                        BillingYear = Convert.ToInt32(b.BillingYear),

                        // ✅ Show SumUnitsImport instead of TotalUnit
                        Units = (decimal?)(b.SumUnitsImport ?? 0),

                        // ✅ If BillAmountInDueDate == 0 or null → use BillAmount
                        Bill = (b.BillAmountInDueDate == null || b.BillAmountInDueDate == 0)
                        ? (decimal?)(b.BillAmount ?? 0)
                        : (decimal?)(b.BillAmountInDueDate ?? 0),
                    })
                    .ToListAsync();

                // Step 5: Split latest and previous year
                var latestYearData = bills
                    .Where(x => x.BillingYear == latestYear)
                    .OrderBy(x => MonthOrder.IndexOf(x.BillingMonth))
                    .ToList();

                var previousYearData = bills
                    .Where(x => x.BillingYear == latestYear - 1)
                    .OrderBy(x => MonthOrder.IndexOf(x.BillingMonth))
                    .ToList();

                // Step 6: Prepare final 12-month sequence (Jan→latestMonth of latestYear, then previous Oct–Dec)
                var finalMonths = MonthOrder.Take(latestMonthIndex + 1)
                    .Select(m => latestYearData.FirstOrDefault(x => x.BillingMonth == m) ??
                                 new { BillingMonth = m, BillingYear = latestYear, Units = (decimal?)0, Bill = (decimal?)0 })
                    .ToList();

                // Add remaining months (from previous year)
                var remainingMonths = MonthOrder.Skip(latestMonthIndex + 1)
                    .Select(m => previousYearData.FirstOrDefault(x => x.BillingMonth == m) ??
                                 new { BillingMonth = m, BillingYear = latestYear - 1, Units = (decimal?)0, Bill = (decimal?)0 })
                    .ToList();

                var fullYearData = finalMonths.Concat(remainingMonths).ToList();

                // Step 7: Final response
                var finalResult = new
                {
                    latestData.ElectricityBillsNetMeter,
                    latestData.CustomerDetail,
                    BillHistory = fullYearData
                };

                return Ok(finalResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving data: " + ex.Message);
            }
        }




    }
}
