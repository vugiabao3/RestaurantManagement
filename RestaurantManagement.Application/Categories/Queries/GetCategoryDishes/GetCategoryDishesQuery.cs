using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace RestaurantManagement.Application
.Categories.Queries.GetCategoryDishes;

public record GetCategoryDishesQuery(
    int CategoryId
)
: IRequest<List<GetCategoryDishesResponse>>;