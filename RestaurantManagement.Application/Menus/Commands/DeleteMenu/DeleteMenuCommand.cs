using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace RestaurantManagement.Application
    .Menus.Commands.DeleteMenu;

public class DeleteMenuCommand
    : IRequest<DeleteMenuResponse>
{
    public int MenuId { get; set; }
}
