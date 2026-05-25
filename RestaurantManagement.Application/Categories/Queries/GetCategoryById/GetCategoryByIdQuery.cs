using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace RestaurantManagement.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery
        : IRequest<GetCategoryByIdResponse>
    {
        public int CategoryId { get; set; }
    }
}