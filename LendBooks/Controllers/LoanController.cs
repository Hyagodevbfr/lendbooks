using ClosedXML.Excel;
using LendBooks.Infra.Data;
using LendBooks.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

    [HttpGet]
    public IActionResult Export() 
    {

        var data = GetData( );

        using(XLWorkbook workBook = new())
        {
            workBook.AddWorksheet(data, "Dados Empréstimos");
            using(MemoryStream memoryStream = new())
            {
                workBook.SaveAs( memoryStream);
                return File(memoryStream.ToArray( ), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet","Emprestimo.xls");
            } 
        } 
    }

    private DataTable GetData()
    {
        DataTable data = new();

        data.TableName = "Loan Data";
        data.Columns.Add("Recebedor", typeof(string));
        data.Columns.Add("Fornecedor",typeof(string));
        data.Columns.Add("Livro",typeof(string));
        data.Columns.Add("Data empréstimo",typeof(DateTime));

        var dataLoans = _dbContext.Loans!.ToList( );
        if(dataLoans.Count > 0)
        {
            dataLoans.ForEach(Loans =>
            {
                data.Rows.Add(Loans.Reciver,Loans.Supplier,Loans.BorrowedBook,Loans.LastUpdate);
            });
        }

        return data;
    }
}
