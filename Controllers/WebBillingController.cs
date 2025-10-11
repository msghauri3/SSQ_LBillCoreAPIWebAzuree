using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBilling_Lahore_ReactCore.Models;

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

        // ✅ Example: api/EBillingNotes?BTNo=BTL-10014&Sector=B
        [HttpGet]
        public async Task<IActionResult> GetEBill(string BTNo, string Sector)
        {
            try
            {
                // ✅ Fetch all bills where BTNo & Sector match
                var result = await _context.EBillingNotes
                    .Where(b => b.REFRENCENOBARCODE == BTNo &&
                                b.Sector.ToLower() == Sector.ToLower())
                    .ToListAsync();

                if (result == null || !result.Any())
                    return NotFound("No record found for given BTNo and Sector.");

                // ✅ Automatically return all fields of EBillingNotes
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving data: " + ex.Message);
            }
        }
    }
}
