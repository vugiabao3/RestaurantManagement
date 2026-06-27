using RestaurantManagement.Domain.Enums;

namespace RestaurantManagement.Domain.Entities;

public class Staff
{
    public int StaffId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public SystemRole Role { get; set; }
    
    // Đã đổi từ ManagedInvoices -> Invoices
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>(); 
}