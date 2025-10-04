using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBilling_Lahore_ReactCore.Models;

namespace WebBilling_Lahore_ReactCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersDetailController : ControllerBase
    {
        private readonly SSQReactCoreContext _context;

        public CustomersDetailController(SSQReactCoreContext context)
        {
            _context = context;
        }

        // ✅ GET: api/CustomersDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomersDetail>>> GetAllCustomers()
        {
            try
            {
                var customers = await _context.CustomersDetail.ToListAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving data: " + ex.Message);
            }
        }

        // ✅ GET: api/CustomersDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomersDetail>> GetCustomerById(int id)
        {
            var customer = await _context.CustomersDetail.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // ✅ POST: api/CustomersDetail
        [HttpPost]
        public async Task<ActionResult> AddCustomer([FromBody] CustomersDetail model)
        {
            if (model == null)
                return BadRequest("Invalid data.");

            try
            {
                _context.CustomersDetail.Add(model);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Record inserted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error inserting record: " + ex.Message);
            }
        }
    }
}
