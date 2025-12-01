using Loan_Management_System.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Loan_Management_System.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // DbSets
        public DbSet<Customer> Customers { get; set; }
        public DbSet<LoanOfficer> LoanOfficers { get; set; }
        public DbSet<LoanRequest> LoanRequests { get; set; }
        public DbSet<BackgroundVerification> BackgroundVerifications { get; set; }
        public DbSet<LoanVerification> LoanVerifications { get; set; }
        public DbSet<FeedbackQuestion> FeedbackQuestions { get; set; }
        public DbSet<CustomerFeedback> CustomerFeedbacks { get; set; }
        public DbSet<HelpReport> HelpReports { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // One-to-one: ApplicationUser -> Customer
            builder.Entity<ApplicationUser>()
                .HasOne(a => a.CustomerProfile)
                .WithOne(c => c.User)
                .HasForeignKey<Customer>(c => c.ApplicationUserId)
                .IsRequired(false);

            // One-to-one: ApplicationUser -> LoanOfficer
            builder.Entity<ApplicationUser>()
                .HasOne(a => a.OfficerProfile)
                .WithOne(o => o.User)
                .HasForeignKey<LoanOfficer>(o => o.ApplicationUserId)
                .IsRequired(false);

            // Customer -> LoanRequests (one-to-many)
            builder.Entity<Customer>()
                .HasMany(c => c.LoanRequests)
                .WithOne(l => l.Customer)
                .HasForeignKey(l => l.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // LoanOfficer -> Assigned LoanRequests (one-to-many)
            builder.Entity<LoanOfficer>()
                .HasMany(o => o.AssignedLoanRequests)
                .WithOne(l => l.AssignedOfficer)
                .HasForeignKey(l => l.AssignedOfficerId)
                .OnDelete(DeleteBehavior.SetNull);

            // LoanRequest -> BackgroundVerification (one-to-one)
            builder.Entity<LoanRequest>()
                .HasOne(l => l.BackgroundVerification)
                .WithOne(b => b.LoanRequest)
                .HasForeignKey<BackgroundVerification>(b => b.LoanRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // LoanRequest -> LoanVerification (one-to-one)
            builder.Entity<LoanRequest>()
                .HasOne(l => l.LoanVerification)
                .WithOne(v => v.LoanRequest)
                .HasForeignKey<LoanVerification>(v => v.LoanRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // LoanOfficer -> Assigned BackgroundVerifications (one-to-many)
            builder.Entity<LoanOfficer>()
                .HasMany(o => o.AssignedBackgroundVerifications)
                .WithOne(b => b.AssignedOfficer)
                .HasForeignKey(b => b.AssignedOfficerId)
                .OnDelete(DeleteBehavior.SetNull);

            // LoanOfficer -> Assigned LoanVerifications (one-to-many)
            builder.Entity<LoanOfficer>()
                .HasMany(o => o.AssignedLoanVerifications)
                .WithOne(v => v.AssignedOfficer)
                .HasForeignKey(v => v.AssignedOfficerId)
                .OnDelete(DeleteBehavior.SetNull);

            // FeedbackQuestion -> CustomerFeedback (one-to-many)
            builder.Entity<FeedbackQuestion>()
                .HasMany(f => f.CustomerFeedbacks)
                .WithOne(c => c.FeedbackQuestion)
                .HasForeignKey(c => c.FeedbackQuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Customer -> CustomerFeedback (one-to-many)
            builder.Entity<Customer>()
                .HasMany(c => c.CustomerFeedbacks)
                .WithOne(f => f.Customer)
                .HasForeignKey(f => f.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
