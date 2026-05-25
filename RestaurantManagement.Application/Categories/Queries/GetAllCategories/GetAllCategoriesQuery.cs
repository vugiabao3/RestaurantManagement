using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace RestaurantManagement.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery
        : IRequest<List<GetAllCategoriesResponse>>
    {
    }
}