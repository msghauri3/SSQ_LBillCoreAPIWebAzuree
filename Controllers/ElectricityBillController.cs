using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        [HttpGet("GetSimpleBill")]
        public IActionResult GetSimpleBill(string btNo)
        {
            var bill = _context.ElectricityBills
                .Where(b => b.BTNo == btNo)
                .Select(b => new ElectricityBill
                {
                    InvoiceNo = b.InvoiceNo,
                    CustomerNo = b.CustomerNo,
                    CustomerName = b.CustomerName,
                    BTNo = b.BTNo,
                    BillingMonth = b.BillingMonth,
                    BillingYear = b.BillingYear,
                    //AmountDueDate = b.DueDate,
                    //AmountAfterDate = b.BillAmountAfterDueDate
                })
                .FirstOrDefault();

            if (bill == null)
                return NotFound(new { message = "No bill found for given BTNo." });

            return Ok(bill);
        }




        [HttpGet("GetBillDetails")]
        public async Task<IActionResult> GetBillDetails(string btNo, string project)
        {
            var result = await (from bill in _context.ElectricityBills
                                join customer in _context.CustomersDetails
                                    on bill.BTNo equals customer.BTNo
                                where bill.BTNo == btNo && customer.Project == project
                                select new
                                {
                                    bill.InvoiceNo,
                                    bill.CustomerNo,
                                    bill.CustomerName,
                                    bill.BTNo,
                                    bill.BillingMonth,
                                    bill.BillingYear,
                                    bill.DueDate,
                                    bill.BillAmountAfterDueDate,
                                    customer.Sector,
                                    customer.Block,
                                    customer.Project
                                    // customer.BillingType (uncomment if needed later)
                                })
                                .FirstOrDefaultAsync();

            if (result == null)
                return NotFound(new { message = "No bill or customer found for given BTNo and Project" });

            return Ok(result);
        }






    }





}
