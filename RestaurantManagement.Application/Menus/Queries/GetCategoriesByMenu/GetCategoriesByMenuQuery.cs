using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MediatR;

namespace RestaurantManagement.Application
.Menus.Queries.GetCategoriesByMenu;

public record GetCategoriesByMenuQuery(
    int MenuId
) : IRequest<List<GetCategoriesByMenuResponse>>;