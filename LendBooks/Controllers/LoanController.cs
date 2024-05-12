using LendBooks.Infra.Data;
using LendBooks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register(LoansModel loan)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Loans?.Add(loan);
            _dbContext.SaveChanges( );
            TempData["SuccessMessage"] = "Cadastro realizado com sucesso";
            return RedirectToAction("Index");
        }
        return View();
    }
    [HttpGet]
    public IActionResult Edit(Guid? id)
    {
        if (id == null)
        {
            return BadRequest( );
        }
        LoansModel loan = _dbContext.Loans?.FirstOrDefault( loan => loan.Id == id )!;
        if(loan == null)
            return BadRequest( );

        return View(loan);
    }

    [HttpPost]
    public IActionResult Edit(LoansModel loan)
    {
        if(ModelState.IsValid)
        {
            _dbContext.Loans!.Update(loan);
            _dbContext.SaveChanges( );
            TempData["SuccessMessage"] = "Edição realizada com sucesso";
            return RedirectToAction("Index");
        }
        return View(loan);
    }

    [HttpGet]
    public IActionResult Delete(Guid? id)
    {
        if(id == null)
        {
            return BadRequest( );
        }
        LoansModel loan = _dbContext.Loans?.FirstOrDefault(loan => loan.Id == id)!;
        if(loan == null)
            return BadRequest( );

        return View(loan);
    }
    [HttpPost]
    public IActionResult Delete(LoansModel loan)
    {
        _dbContext.Loans!.Remove(loan);
        _dbContext.SaveChanges( );
        TempData["SuccessMessage"] = "Deleção realizada com sucesso";
        return RedirectToAction("Index");     
    }

}
