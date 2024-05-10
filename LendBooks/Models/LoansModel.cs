namespace LendBooks.Models;

public class LoansModel
{
    public Guid Id { get; set; }
    public string Reciver { get; set; } = null!;
    public string Supplier { get; set; } = null!;
    public string BorrowedBook { get; set; } = null!;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
