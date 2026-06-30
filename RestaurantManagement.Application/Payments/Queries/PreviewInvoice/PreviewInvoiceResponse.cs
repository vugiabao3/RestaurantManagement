using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RestaurantManagement.Application.Payments.Queries.PreviewInvoice;


public class PreviewInvoiceResponse
{
    public int OrderId { get; set; }

    public decimal Total { get; set; }

    public decimal Discount { get; set; }

    public decimal FinalAmount { get; set; }

    public bool HasMemberCard { get; set; }

    public string? MemberName { get; set; }
}