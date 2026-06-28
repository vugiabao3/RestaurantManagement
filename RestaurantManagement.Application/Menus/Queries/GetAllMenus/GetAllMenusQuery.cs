using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace RestaurantManagement.Application.Menus.Queries.GetAllMenus
{
 

    public class GetAllMenusQuery : IRequest<List<MenuResponse>>
    {
    }
}
