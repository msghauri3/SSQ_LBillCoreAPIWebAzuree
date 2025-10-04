using WebBilling_Lahore_ReactCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace WebBilling_Lahore_ReactCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebBillingController : ControllerBase
    {
        private readonly WebBillingContext _context;

        public WebBillingController(WebBillingContext context)
        {
            _context = context;
        }

        // ✅ GetBill using query parameters
        [HttpGet("GetBill")]
        public async Task<IActionResult> GetBill(
            [FromQuery] string btNo,
            [FromQuery] string sector,
            [FromQuery] string billingType)
        {
            if (string.IsNullOrEmpty(btNo) || string.IsNullOrEmpty(sector))
            {
                return BadRequest(new { message = "btNo and project are required." });
            }

            var bills = await _context.EBillingNotes
                .Where(b => b.REFRENCENOBARCODE == btNo
                && b.Sector.ToLower() == sector.ToLower() 
                

                ) // ✅ Filter by btNo
                .Select(b => new
                {
                    b.Customer_Name,
                    b.Plot_Number,
                    b.Meter_Number,
                    b.paymentstatus,
                    b.totalunit,
                    b.GTotalAmount,
                    b.TotalElecCharges,
                    b.GTotal,
                    b.InvoiceNo
                })
                .ToListAsync();

            if (!bills.Any())
                return NotFound(new { message = "No bill found for given BTNo and Project." });

            return Ok(bills);
        }
    }
}
