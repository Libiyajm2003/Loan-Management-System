using Loan_Management_System.Database;
using Loan_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loan_Management_System.Repositories
{
    public class HelpReportRepository : IHelpReportRepository
    {
        private readonly ApplicationDbContext _context;
        public HelpReportRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<HelpReport>> GetAllAsync() => await _context.HelpReports.ToListAsync();
        public async Task<HelpReport?> GetByIdAsync(int id) => await _context.HelpReports.FindAsync(id);

        public async Task<HelpReport> AddAsync(HelpReport report)
        {
            _context.HelpReports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<bool> UpdateAsync(HelpReport model)
        {
            var hr = await _context.HelpReports.FindAsync(model.Id);
            if (hr == null) return false;
            hr.Status = model.Status;
            hr.Remarks = model.Remarks;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
