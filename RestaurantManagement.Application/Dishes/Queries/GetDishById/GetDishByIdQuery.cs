using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace RestaurantManagement.Application
    .Dishes.Queries.GetDishById;

public class GetDishByIdQuery
    : IRequest<GetDishByIdResponse>
{
    public int DishId { get; set; }
}