using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;


namespace RestaurantManagement.Application.Payments.Queries.PreviewInvoice;

public class PreviewInvoiceQuery
    : IRequest<PreviewInvoiceResponse>
{
    public int OrderId { get; set; }
}