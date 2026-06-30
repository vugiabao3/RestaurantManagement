using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace RestaurantManagement.Application.Orders.Queries.GetCart;

public class GetCartQuery : IRequest<GetCartResponse>
{
    public int CustomerId { get; set; }
}