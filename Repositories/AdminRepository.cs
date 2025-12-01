using Loan_Management_System.Database;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan_Management_System.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===================== Customers =====================

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync() =>
            await _context.Customers.ToListAsync();

        public async Task<bool> ApproveCustomerAsync(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null) return false;
            customer.IsApproved = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectCustomerAsync(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null) return false;
            customer.IsApproved = false;
            await _context.SaveChangesAsync();
            return true;
        }

        // ===================== Loan Officers =====================

        public async Task<IEnumerable<LoanOfficer>> GetAllLoanOfficersAsync() =>
            await _context.LoanOfficers.ToListAsync();

        public async Task<bool> ApproveLoanOfficerAsync(int officerId)
        {
            var officer = await _context.LoanOfficers.FindAsync(officerId);
            if (officer == null) return false;
            officer.IsApproved = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectLoanOfficerAsync(int officerId)
        {
            var officer = await _context.LoanOfficers.FindAsync(officerId);
            if (officer == null) return false;
            officer.IsApproved = false;
            await _context.SaveChangesAsync();
            return true;
        }

        // Assign Loan Officer to Background Verification
        public async Task<bool> AssignOfficerToBackgroundVerification(int verificationId, int officerId)
        {
            var verification = await _context.BackgroundVerifications.FindAsync(verificationId);
            if (verification == null) return false;
            verification.AssignedOfficerId = officerId;
            await _context.SaveChangesAsync();
            return true;
        }

        // Assign Loan Officer to Loan Verification
        public async Task<bool> AssignOfficerToLoanVerification(int verificationId, int officerId)
        {
            var verification = await _context.LoanVerifications.FindAsync(verificationId);
            if (verification == null) return false;
            verification.AssignedOfficerId = officerId;
            await _context.SaveChangesAsync();
            return true;
        }

        // ===================== Loan Requests =====================

        public async Task<IEnumerable<LoanRequest>> GetAllLoanRequestsAsync() =>
            await _context.LoanRequests
                .Include(l => l.Customer)
                .Include(l => l.AssignedOfficer)
                .ToListAsync();

        // ===================== Background Verifications =====================

        public async Task<IEnumerable<BackgroundVerification>> GetAllBackgroundVerificationsAsync() =>
            await _context.BackgroundVerifications
                .Include(b => b.LoanRequest)
                .Include(b => b.AssignedOfficer)
                .ToListAsync();

        public async Task<bool> UpdateBackgroundVerificationAsync(BackgroundVerification verification)
        {
            _context.BackgroundVerifications.Update(verification);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBackgroundVerificationAsync(int id)
        {
            var verification = await _context.BackgroundVerifications.FindAsync(id);
            if (verification == null) return false;
            _context.BackgroundVerifications.Remove(verification);
            await _context.SaveChangesAsync();
            return true;
        }

        // ===================== Loan Verifications =====================

        public async Task<IEnumerable<LoanVerification>> GetAllLoanVerificationsAsync() =>
            await _context.LoanVerifications
                .Include(l => l.LoanRequest)
                .Include(l => l.AssignedOfficer)
                .ToListAsync();

        public async Task<bool> UpdateLoanVerificationAsync(LoanVerification verification)
        {
            _context.LoanVerifications.Update(verification);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLoanVerificationAsync(int id)
        {
            var verification = await _context.LoanVerifications.FindAsync(id);
            if (verification == null) return false;
            _context.LoanVerifications.Remove(verification);
            await _context.SaveChangesAsync();
            return true;
        }

        // ===================== Help Reports =====================

        public async Task<HelpReport> AddHelpReportAsync(HelpReport report)
        {
            _context.HelpReports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<HelpReport>> GetAllHelpReportsAsync() =>
            await _context.HelpReports.ToListAsync();

        public async Task<bool> UpdateHelpReportAsync(HelpReport report)
        {
            _context.HelpReports.Update(report);
            await _context.SaveChangesAsync();
            return true;
        }

        // ===================== Feedback =====================

        public async Task<FeedbackQuestion> AddFeedbackQuestionAsync(FeedbackQuestion question)
        {
            _context.FeedbackQuestions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<IEnumerable<FeedbackQuestion>> GetAllFeedbackQuestionsAsync() =>
            await _context.FeedbackQuestions.ToListAsync();

        public async Task<bool> UpdateFeedbackQuestionAsync(FeedbackQuestion question)
        {
            _context.FeedbackQuestions.Update(question);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CustomerFeedback>> GetCustomerFeedbackAsync() =>
            await _context.CustomerFeedbacks
                .Include(f => f.Customer)
                .Include(f => f.FeedbackQuestion)
                .ToListAsync();
    }
}
