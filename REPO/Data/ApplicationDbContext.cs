using Microsoft.EntityFrameworkCore;
using APP.Entities;
namespace REPO.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Stakeholder> Stakeholders { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }
    public DbSet<CashFlow> CashFlows { get; set; }
    public DbSet<Progress> Progresses { get; set; }
    public DbSet<SubAccounts> SubAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=app.db");
    }
}