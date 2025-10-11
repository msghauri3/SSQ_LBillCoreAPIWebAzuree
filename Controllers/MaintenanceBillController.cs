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

        public MaintenanceBillController(SSQReactCoreContext context)
        {
            _context = context;
        }

        // GET: api/MaintenanceBills
        [HttpGet]
        public async Task<IActionResult> GetElectricityBills(string BTNo, string Project)
        {
            try
            {
                var result = await(from bill in _context.MaintenanceBills
                                   join cust in _context.CustomersMaintenance
                                   on bill.BTNo equals cust.BTNo
                                   where cust.Project == Project && bill.BTNo == BTNo
                                   select new
                                   {
                                       MaintenanceBills = bill,
                                       CustomersMaintenance = cust
                                   }).ToListAsync();

                if (result == null || !result.Any())
                    return NotFound("No record found for given BTNo and Project.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving data: " + ex.Message);
            }
        }
    }
}
