using Loan_Management_System.Models;
using Loan_Management_System.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Loan_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly ILoanOfficerRepository _officerRepo;
        private readonly ILoanRepository _loanRepo;
        private readonly IBackgroundVerificationRepository _bgRepo;
        private readonly ILoanVerificationRepository _loanVerRepo;
        private readonly IHelpReportRepository _helpRepo;
        private readonly IFeedbackRepository _feedbackRepo;

        public AdminController(
            ICustomerRepository customerRepo,
            ILoanOfficerRepository officerRepo,
            ILoanRepository loanRepo,
            IBackgroundVerificationRepository bgRepo,
            ILoanVerificationRepository loanVerRepo,
            IHelpReportRepository helpRepo,
            IFeedbackRepository feedbackRepo)
        {
            _customerRepo = customerRepo;
            _officerRepo = officerRepo;
            _loanRepo = loanRepo;
            _bgRepo = bgRepo;
            _loanVerRepo = loanVerRepo;
            _helpRepo = helpRepo;
            _feedbackRepo = feedbackRepo;
        }

        #region Customer Management
        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerRepo.GetAllAsync();
            return Ok(customers);
        }

        [HttpPut("customers/{id}/approve")]
        public async Task<IActionResult> ApproveCustomer(int id)
        {
            var result = await _customerRepo.ApproveCustomerAsync(id);
            if (!result) return NotFound("Customer not found");
            return Ok(new { Message = "Customer approved" });
        }

        [HttpPut("customers/{id}/reject")]
        public async Task<IActionResult> RejectCustomer(int id)
        {
            var result = await _customerRepo.RejectCustomerAsync(id);
            if (!result) return NotFound("Customer not found");
            return Ok(new { Message = "Customer rejected" });
        }
        #endregion

        #region Loan Officer Management
        [HttpGet("loan-officers")]
        public async Task<IActionResult> GetAllLoanOfficers()
        {
            var officers = await _officerRepo.GetAllAsync();
            return Ok(officers);
        }

        [HttpPut("loan-officers/{id}/approve")]
        public async Task<IActionResult> ApproveLoanOfficer(int id)
        {
            var result = await _officerRepo.ApproveLoanOfficerAsync(id);
            if (!result) return NotFound("Loan officer not found");
            return Ok(new { Message = "Loan officer approved" });
        }

        [HttpPut("loan-officers/{id}/reject")]
        public async Task<IActionResult> RejectLoanOfficer(int id)
        {
            var result = await _officerRepo.RejectLoanOfficerAsync(id);
            if (!result) return NotFound("Loan officer not found");
            return Ok(new { Message = "Loan officer rejected" });
        }

        [HttpPost("loan-officers/assign-bg/{verificationId:int}/{officerId:int}")]
        public async Task<IActionResult> AssignOfficerForBackgroundVerification(int verificationId, int officerId)
        {
            var result = await _bgRepo.AssignOfficerAsync(verificationId, officerId);
            if (!result) return NotFound("Loan request or officer not found");
            return Ok(new { Message = "Officer assigned for background verification" });
        }

        [HttpPost("loan-officers/assign-loan-verification/{verificationId:int}/{officerId:int}")]
        public async Task<IActionResult> AssignOfficerForLoanVerification(int verificationId, int officerId)
        {
            var result = await _loanVerRepo.AssignOfficerAsync(verificationId, officerId);
            if (!result) return NotFound("Loan request or officer not found");
            return Ok(new { Message = "Officer assigned for loan verification" });
        }
        #endregion

        #region Loan Requests
        [HttpGet("loans")]
        public async Task<IActionResult> GetAllLoanRequests()
        {
            var loans = await _loanRepo.GetAllAsync();
            return Ok(loans);
        }
        #endregion

        #region Background Verification
        [HttpGet("background-verifications")]
        public async Task<IActionResult> GetBackgroundVerifications()
        {
            var list = await _bgRepo.GetAllAsync();
            return Ok(list);
        }

        [HttpPut("background-verifications/{id}")]
        public async Task<IActionResult> UpdateBackgroundVerification(int id, [FromBody] BackgroundVerification model)
        {
            var result = await _bgRepo.UpdateAsync(id, model);
            if (!result) return NotFound("Not found");
            return Ok(new { Message = "Updated successfully" });
        }

        [HttpDelete("background-verifications/{id}")]
        public async Task<IActionResult> DeleteBackgroundVerification(int id)
        {
            var result = await _bgRepo.DeleteAsync(id);
            if (!result) return NotFound("Not found");
            return Ok(new { Message = "Deleted successfully" });
        }
        #endregion

        #region Loan Verification
        [HttpGet("loan-verifications")]
        public async Task<IActionResult> GetLoanVerifications()
        {
            var list = await _loanVerRepo.GetAllAsync();
            return Ok(list);
        }

        [HttpPut("loan-verifications/{id}")]
        public async Task<IActionResult> UpdateLoanVerification(int id, [FromBody] LoanVerification model)
        {
            var result = await _loanVerRepo.UpdateAsync(id, model);
            if (!result) return NotFound("Not found");
            return Ok(new { Message = "Updated successfully" });
        }

        [HttpDelete("loan-verifications/{id}")]
        public async Task<IActionResult> DeleteLoanVerification(int id)
        {
            var result = await _loanVerRepo.DeleteAsync(id);
            if (!result) return NotFound("Not found");
            return Ok(new { Message = "Deleted successfully" });
        }
        #endregion

        #region Help Report
        [HttpGet("help-reports")]
        public async Task<IActionResult> GetHelpReports()
        {
            var reports = await _helpRepo.GetAllAsync();
            return Ok(reports);
        }

        [HttpPut("help-reports/{id}")]
        public async Task<IActionResult> UpdateHelpReport(int id, [FromBody] HelpReport model)
        {
            model.Id = id;
            var result = await _helpRepo.UpdateAsync(model);
            if (!result) return NotFound("Not found");
            return Ok(new { Message = "Updated successfully" });
        }
        #endregion

        #region Feedback
        [HttpPost("feedback-questions")]
        public async Task<IActionResult> AddFeedbackQuestion([FromBody] FeedbackQuestion model)
        {
            await _feedbackRepo.AddQuestionAsync(model);
            return Ok(new { Message = "Feedback question added" });
        }

        [HttpGet("feedback-questions")]
        public async Task<IActionResult> GetFeedbackQuestions()
        {
            var questions = await _feedbackRepo.GetQuestionsAsync();
            return Ok(questions);
        }

        [HttpPut("feedback-questions/{id}")]
        public async Task<IActionResult> UpdateFeedbackQuestion(int id, [FromBody] FeedbackQuestion model)
        {
            var result = await _feedbackRepo.UpdateQuestionAsync(id, model);
            if (!result) return NotFound("Not found");
            return Ok(new { Message = "Updated successfully" });
        }

        [HttpGet("customer-feedback")]
        public async Task<IActionResult> GetCustomerFeedback()
        {
            var feedback = await _feedbackRepo.GetCustomerFeedbackAsync();
            return Ok(feedback);
        }
        #endregion
    }
}
