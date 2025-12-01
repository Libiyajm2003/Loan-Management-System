using Loan_Management_System.Database;
using Loan_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Loan_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Officer")]
    public class OfficerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OfficerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all loans assigned to the logged-in officer
        [HttpGet("assigned-loans")]
        public async Task<IActionResult> GetAssignedLoans()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var officer = await _context.LoanOfficers.FirstOrDefaultAsync(o => o.ApplicationUserId == userId);

            if (officer == null)
                return BadRequest(new { Message = "Officer profile not found" });

            var loans = await _context.LoanRequests
                .Where(l => l.AssignedOfficerId == officer.Id)
                .Include(l => l.Customer)
                .Include(l => l.LoanVerification)
                .Include(l => l.BackgroundVerification)
                .ToListAsync();

            return Ok(loans);
        }

        // Update loan verification for a specific loan
        [HttpPost("update-loan-verification/{loanId}")]
        public async Task<IActionResult> UpdateLoanVerification(int loanId, [FromBody] LoanVerification model)
        {
            if (model == null)
                return BadRequest(new { Message = "Invalid data" });

            var loan = await _context.LoanRequests.FindAsync(loanId);
            if (loan == null)
                return NotFound(new { Message = "Loan not found" });

            model.LoanRequestId = loanId;
            _context.LoanVerifications.Add(model);
            loan.LoanVerification = model;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Loan verification updated successfully", LoanVerification = model });
        }

        // Update background verification for a specific loan
        [HttpPost("update-background-verification/{loanId}")]
        public async Task<IActionResult> UpdateBackgroundVerification(int loanId, [FromBody] BackgroundVerification model)
        {
            if (model == null)
                return BadRequest(new { Message = "Invalid data" });

            var loan = await _context.LoanRequests.FindAsync(loanId);
            if (loan == null)
                return NotFound(new { Message = "Loan not found" });

            model.LoanRequestId = loanId;
            _context.BackgroundVerifications.Add(model);
            loan.BackgroundVerification = model;

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Background verification updated successfully", BackgroundVerification = model });
        }

        // Get all help reports
        [HttpGet("help-reports")]
        public async Task<IActionResult> GetHelpReports()
        {
            var reports = await _context.HelpReports.ToListAsync();
            return Ok(reports);
        }
    }
}