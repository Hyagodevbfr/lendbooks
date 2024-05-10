using LendBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace LendBooks.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}

    public DbSet<LoansModel>? Loans { get; set; }
}
