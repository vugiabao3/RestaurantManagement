using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace RestaurantManagement.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand
        : IRequest<DeleteCategoryResponse>
    {
        public int CategoryId { get; set; }
    }
}
