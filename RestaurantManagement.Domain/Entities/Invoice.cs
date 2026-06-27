using RestaurantManagement.Domain.Enums; // Thêm dòng này

namespace RestaurantManagement.Domain.Entities;

public class Invoice
{
    public int InvoiceId { get; set; }
    public int OrderId { get; set; }
    public int CashierId { get; set; }
    public int? MemberId { get; set; } 
    
    public DateTime PaymentTime { get; set; } = DateTime.Now;
    
    // BỔ SUNG: Phương thức thanh toán
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash; 
    
    public decimal SubTotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    
    // BỔ SUNG: Ghi chú lý do giảm giá (VD: "Mã KM Mua 1 Tặng 1", "VIP -10%")
    public string? DiscountReason { get; set; } 
    
    public decimal FinalAmount { get; set; }

    // Navigation Properties...
    public CustomerOrder? Order { get; set; }
    public Staff? Cashier { get; set; }
    public MemberCard? Member { get; set; }
}