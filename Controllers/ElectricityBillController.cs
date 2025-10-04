using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // ✅ GET: api/ElectricityBill?BTNo=BT-1001&Project=SSQ
        [HttpGet]
        public async Task<IActionResult> GetElectricityBills(string BTNo, string Project)
        {
            try
            {
                var result = await (from bill in _context.ElectricityBills
                                    join cust in _context.CustomersDetail
                                    on bill.BTNo equals cust.BTNo
                                    where cust.Project == Project && bill.BTNo == BTNo
                                    select new
                                    {
                                        ElectricityBill = bill,
                                        CustomerDetail = cust
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
