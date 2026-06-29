namespace RestaurantManagement.Domain.Entities;

public enum PaymentMethod
{
    Cash,
    CreditCard,
    BankTransfer
}

public class Invoice
{
    public int InvoiceId { get; set; }
    public int OrderId { get; set; }
    public int CashierId { get; set; }
    public int? MemberId { get; set; } 
    
    public DateTime PaymentTime { get; set; } = DateTime.Now;
    
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash; 
    
    public decimal SubTotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    
    public string? DiscountReason { get; set; } 
    
    public decimal FinalAmount { get; set; }

    public CustomerOrder? Order { get; set; }
    public Cashier? Cashier { get; set; } // Đã đổi Staff thành Cashier
    public MemberCard? Member { get; set; }
}