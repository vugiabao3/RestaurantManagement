namespace RestaurantManagement.Domain.Entities;

public class Cashier
{
    public int CashierId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    
    // Đã loại bỏ hoàn toàn SystemRole theo yêu cầu
    
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>(); 
}