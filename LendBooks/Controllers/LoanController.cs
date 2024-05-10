using LendBooks.Infra.Data;
using LendBooks.Models;
using Microsoft.AspNetCore.Mvc;

namespace LendBooks.Controllers;
public class LoanController: Controller
{
    readonly private ApplicationDbContext _dbContext;
    public LoanController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        IEnumerable<LoansModel> loans = _dbContext.Loans!;
        return View(loans);
    }
}
