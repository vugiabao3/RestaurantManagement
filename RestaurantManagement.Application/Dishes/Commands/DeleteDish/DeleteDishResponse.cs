using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace RestaurantManagement.Application.Dishes.Commands.DeleteDish;

public class DeleteDishResponse
{
    public string Message { get; set; }
        = string.Empty;
}