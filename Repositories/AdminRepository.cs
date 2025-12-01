using Loan_Management_System.Database;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;


namespace Loan_Management_System.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoanRequest>> GetAllLoanRequestsAsync()
        {
            return await _context.LoanRequests
                .Include(l => l.Customer)
                .Include(l => l.AssignedOfficer)
                .ToListAsync();
        }

        public async Task<HelpReport> AddHelpReportAsync(HelpReport report)
        {
            _context.HelpReports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<IEnumerable<Feedback>> GetCustomerFeedbackAsync()
        {
            return await _context.Feedbacks.Include(f => f.Customer).ToListAsync();
        }
    }
}