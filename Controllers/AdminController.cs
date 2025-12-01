using Loan_Management_System.Database;
using Loan_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Loan_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("approve-customer/{userId}")]
        public async Task<IActionResult> ApproveCustomer(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            var existing = await _context.Customers.FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
            if (existing == null)
            {
                var customer = new Customer
                {
                    ApplicationUserId = userId,
                    Address = "Default Address",   // Required field
                    Aadhaar = "123456789012",      // Required field
                    PAN = "ABCDE1234F"             // Optional, can be dummy
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }

            return Ok(new { Message = "Customer approved" });
        }

        // Reject a customer
        [HttpPost("reject-customer/{userId}")]
        public async Task<IActionResult> RejectCustomer(string userId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
            return Ok(new { Message = "Customer rejected" });
        }

        // Approve a loan officer
        [HttpPost("approve-officer/{userId}")]
        public async Task<IActionResult> ApproveOfficer(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            var existing = await _context.LoanOfficers.FirstOrDefaultAsync(o => o.ApplicationUserId == userId);
            if (existing == null)
            {
                var officer = new LoanOfficer
                {
                    ApplicationUserId = userId,
                    Region = "",
                    IsApproved = true
                };
                _context.LoanOfficers.Add(officer);
            }
            else
            {
                existing.IsApproved = true;
            }

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Officer approved" });
        }

        // Reject a loan officer
        [HttpPost("reject-officer/{userId}")]
        public async Task<IActionResult> RejectOfficer(string userId)
        {
            var officer = await _context.LoanOfficers.FirstOrDefaultAsync(o => o.ApplicationUserId == userId);
            if (officer != null)
            {
                _context.LoanOfficers.Remove(officer);
                await _context.SaveChangesAsync();
            }
            return Ok(new { Message = "Officer rejected" });
        }

        // View all loan requests
        [HttpGet("loan-requests")]
        public async Task<IActionResult> GetLoanRequests()
        {
            var loans = await _context.LoanRequests
                .Include(l => l.Customer)
                .Include(l => l.AssignedOfficer)
                .ToListAsync();

            return Ok(loans);
        }

        // Assign officer to loan request
        [HttpPost("assign-officer/{loanRequestId}/{officerId}")]
        public async Task<IActionResult> AssignOfficer(int loanRequestId, int officerId)
        {
            var loan = await _context.LoanRequests.FindAsync(loanRequestId);
            if (loan == null) return NotFound(new { Message = "Loan request not found" });

            loan.AssignedOfficerId = officerId;
            loan.Status = LoanStatus.UnderVerification;
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Officer assigned successfully" });
        }

        // CRUD for Help Reports
        [HttpGet("help-reports")]
        public async Task<IActionResult> GetHelpReports()
        {
            var reports = await _context.HelpReports.ToListAsync();
            return Ok(reports);
        }

        [HttpPost("help-reports")]
        public async Task<IActionResult> CreateHelpReport([FromBody] HelpReport model)
        {
            _context.HelpReports.Add(model);
            await _context.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("help-reports/{id}")]
        public async Task<IActionResult> UpdateHelpReport(int id, [FromBody] HelpReport model)
        {
            var report = await _context.HelpReports.FindAsync(id);
            if (report == null) return NotFound(new { Message = "Report not found" });

            report.Title = model.Title;
            report.Description = model.Description;
            await _context.SaveChangesAsync();

            return Ok(report);
        }

        [HttpDelete("help-reports/{id}")]
        public async Task<IActionResult> DeleteHelpReport(int id)
        {
            var report = await _context.HelpReports.FindAsync(id);
            if (report == null) return NotFound(new { Message = "Report not found" });

            _context.HelpReports.Remove(report);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Help report deleted" });
        }

        // View customer feedback
        [HttpGet("customer-feedback")]
        public async Task<IActionResult> GetCustomerFeedback()
        {
            var feedbacks = await _context.Feedbacks
                .Include(f => f.Customer)
                .ToListAsync();

            return Ok(feedbacks);
        }
    }
}