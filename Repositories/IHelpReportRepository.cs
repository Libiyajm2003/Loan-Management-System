using Loan_Management_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loan_Management_System.Repositories
{
    public interface IHelpReportRepository
    {
        Task<IEnumerable<HelpReport>> GetAllAsync();
        Task<HelpReport?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(HelpReport model);
        Task<HelpReport> AddAsync(HelpReport report);
    }
}
