using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace RestaurantManagement.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand
        : IRequest<UpdateCategoryResponse>
    {
        public int CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool Status { get; set; }

        public int? MenuId { get; set; }
    }
}