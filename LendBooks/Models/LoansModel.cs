using System.ComponentModel.DataAnnotations;

namespace LendBooks.Models;

public class LoansModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Digite o nome do recebedor!")]
    public string? Reciver { get; set; }

    [Required(ErrorMessage = "Digite o nome do fornecedor!")]
    public string? Supplier { get; set; }

    [Required(ErrorMessage = "Digite o nome do livro emprestado!")]
    public string? BorrowedBook { get; set; }
    public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
}
