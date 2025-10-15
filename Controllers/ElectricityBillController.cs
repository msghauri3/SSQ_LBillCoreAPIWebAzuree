using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebBilling_Lahore_ReactCore.Models;

namespace WebBilling_Lahore_ReactCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityBillController : ControllerBase
    {
        private readonly SSQReactCoreContext _context;

        public ElectricityBillController(SSQReactCoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetElectricityBills(string BTNo, string Project)
        {
            try
            {
                // Step 1: Get latest bill with customer details
                var latestData = await (from bill in _context.ElectricityBills
                                        join cust in _context.CustomersDetail
                                        on bill.BTNo equals cust.BTNo
                                        where bill.BTNo == BTNo && cust.Project == Project
                                        orderby Convert.ToInt32(bill.BillingYear) descending,
                                                bill.BillingMonth descending
                                        select new
                                        {
                                            ElectricityBill = bill,
                                            CustomerDetail = cust
                                        })
                                        .FirstOrDefaultAsync();

                if (latestData == null)
                    return NotFound("No record found for given BTNo and Project.");

                // Step 2: Get the latest bill year and month
                int latestYear = Convert.ToInt32(latestData.ElectricityBill.BillingYear);
                string latestMonth = latestData.ElectricityBill.BillingMonth;

                // Step 3: Month order
                var monthOrder = new List<string>
                {
                    "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };

                int latestMonthIndex = monthOrder.IndexOf(latestMonth);

                // Step 4: Fetch both years' data
                var bills = await _context.ElectricityBills
                    .Where(b => b.BTNo == BTNo &&
                                (Convert.ToInt32(b.BillingYear) == latestYear ||
                                 Convert.ToInt32(b.BillingYear) == latestYear - 1))
                    .Select(b => new
                    {
                        BillingMonth = b.BillingMonth,
                        BillingYear = Convert.ToInt32(b.BillingYear),
                        Units = b.TotalUnit ?? 0,
                        Bill = b.BillAmount ?? 0,
                        Payment = b.PaymentStatus == "paid" ? (b.BillAmount ?? 0) : 0m
                    })
                    .ToListAsync();

                // Step 5: Split latest and previous year
                var latestYearData = bills
                    .Where(x => x.BillingYear == latestYear)
                    .OrderBy(x => monthOrder.IndexOf(x.BillingMonth))
                    .ToList();

                var previousYearData = bills
                    .Where(x => x.BillingYear == latestYear - 1)
                    .OrderBy(x => monthOrder.IndexOf(x.BillingMonth))
                    .ToList();

                // Step 6: Prepare final 12-month sequence (Jan→latestMonth of latestYear, then previous Oct–Dec)
                var finalMonths = monthOrder.Take(latestMonthIndex + 1)
                    .Select(m => latestYearData.FirstOrDefault(x => x.BillingMonth == m) ??
                                 new { BillingMonth = m, BillingYear = latestYear, Units = 0m, Bill = 0m, Payment = 0m })
                    .ToList();

                // Add remaining months (from previous year)
                var remainingMonths = monthOrder.Skip(latestMonthIndex + 1)
                    .Select(m => previousYearData.FirstOrDefault(x => x.BillingMonth == m) ??
                                 new { BillingMonth = m, BillingYear = latestYear - 1, Units = 0m, Bill = 0m, Payment = 0m })
                    .ToList();

                var fullYearData = finalMonths.Concat(remainingMonths).ToList();

                // Step 7: Final response
                var finalResult = new
                {
                    latestData.ElectricityBill,
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
