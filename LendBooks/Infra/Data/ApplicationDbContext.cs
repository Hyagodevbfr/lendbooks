using Microsoft.EntityFrameworkCore;

namespace LendBooks.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}
}
