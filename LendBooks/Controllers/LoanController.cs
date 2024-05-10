using Microsoft.AspNetCore.Mvc;

namespace LendBooks.Controllers;
public class LoanController: Controller
{
    public IActionResult Index()
    {
        return View( );
    }
}
