using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace RestaurantManagement.Application.Dishes.Commands.DeleteDish;

public class DeleteDishCommand
    : IRequest<DeleteDishResponse>
{
    public int DishId { get; set; }
}