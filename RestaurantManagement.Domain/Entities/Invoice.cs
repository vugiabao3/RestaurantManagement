using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Domain.Enums;
namespace RestaurantManagement.Domain.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int? MemberId { get; set; }

        public MemberCard? MemberCard { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal FinalAmount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public DateTime PaidAt { get; set; }
    }
}
