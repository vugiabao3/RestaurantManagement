using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace RestaurantManagement.Application
    .Menus.Commands.CreateMenu;

public class CreateMenuCommand
    : IRequest<CreateMenuResponse>
{
    public string Name { get; set; }
        = string.Empty;

    public string Description { get; set; }
        = string.Empty;

    public bool Status { get; set; }
}
