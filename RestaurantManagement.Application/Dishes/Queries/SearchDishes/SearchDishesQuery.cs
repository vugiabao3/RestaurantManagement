using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace RestaurantManagement.Application
    .Dishes.Queries.SearchDishes;

public class SearchDishesQuery
    : IRequest<List<SearchDishesResponse>>
{
    public string Keyword { get; set; }
        = string.Empty;
}