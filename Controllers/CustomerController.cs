using Loan_Management_System.Models;
using Loan_Management_System.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Loan_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Customer")] // Only approved customers can apply
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly ILoanRepository _loanRepo;

        public CustomerController(ICustomerRepository customerRepo, ILoanRepository loanRepo)
        {
            _customerRepo = customerRepo;
            _loanRepo = loanRepo;
        }

        // Apply for a new loan
        [HttpPost("apply-loan")]
        public async Task<IActionResult> ApplyLoan([FromBody] LoanRequest model)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(model.Purpose))
                return BadRequest(new { Message = "Purpose is required" });

            if (model.Amount <= 0)
                return BadRequest(new { Message = "Amount must be greater than zero" });

            if (model.TenureMonths <= 0)
                return BadRequest(new { Message = "TenureMonths must be greater than zero" });

            // Get logged-in customer
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = await _customerRepo.GetByUserIdAsync(userId);

            if (customer == null)
                return BadRequest(new { Message = "You are not an approved customer" });

            // Assign customer ID and initial status
            model.CustomerId = customer.Id;
            model.Status = LoanStatus.Pending;

            // Prevent EF from trying to save navigation properties
            model.Customer = null;
            model.AssignedOfficer = null;
            model.LoanVerification = null;
            model.BackgroundVerification = null;

            // Save loan request
            var result = await _loanRepo.ApplyLoanAsync(model);

            // Return clean response
            return Ok(new
            {
                Message = "Loan request submitted successfully",
                LoanRequest = new
                {
                    result.Id,
                    result.CustomerId,
                    result.Amount,
                    result.TenureMonths,
                    result.Purpose,
                    result.Status
                }
            });
        }
    }
}
