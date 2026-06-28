using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace RestaurantManagement.Application
    .Dishes.Queries.GetAllDishes;

public class GetAllDishesQuery
    : IRequest<List<GetAllDishesResponse>>
{
}