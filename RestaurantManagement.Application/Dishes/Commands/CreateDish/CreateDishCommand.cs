using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace RestaurantManagement.Application.Dishes.Commands.CreateDish;

public class CreateDishCommand
    : IRequest<CreateDishResponse>
{
    public string Name { get; set; }
        = string.Empty;

    public decimal Price { get; set; }

    public string Description { get; set; }
        = string.Empty;

    public bool Status { get; set; }
    public string ImageUrl { get; set; }


    public int CategoryId { get; set; }
}