using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBilling_Lahore_ReactCore.Models;

namespace WebBilling_Lahore_ReactCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceBillController : ControllerBase
    {
        private readonly SSQReactCoreContext _context;

        // ✅ Same month order as Electricity
        private static readonly List<string> MonthOrder = new()
        {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };

        public MaintenanceBillController(SSQReactCoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetLatestMaintenanceBill(string BTNo, string Project)
        {
            try
            {
                // ✅ Step 1: Get latest bill using Year + Month order
                var latestData = (from bill in _context.MaintenanceBills
                                  join cust in _context.CustomersMaintenance
                                  on bill.BTNo equals cust.BTNo
                                  where bill.BTNo == BTNo && cust.Project == Project
                                  select new
                                  {
                                      MaintenanceBill = bill,
                                      CustomerMaintenance = cust
                                  })
                                  .AsEnumerable() // 👈 MonthOrder client-side sorting
                                  .OrderByDescending(x => Convert.ToInt32(x.MaintenanceBill.BillingYear))
                                  .ThenByDescending(x => MonthOrder.IndexOf(x.MaintenanceBill.BillingMonth))
                                  .FirstOrDefault();

                if (latestData == null)
                    return NotFound("No record found for given BTNo and Project.");

                // ✅ Step 2: Final response (ONLY latest bill)
                var result = new
                {
                    latestData.MaintenanceBill,
                    latestData.CustomerMaintenance
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving data: " + ex.Message);
            }
        }
    }
}
