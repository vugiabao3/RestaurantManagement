using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application
.Menus.Queries.GetCategoriesByMenu;

public class GetCategoriesByMenuResponse
{
    public int CategoryId { get; set; }

    public string Name { get; set; }
        = string.Empty;

    public string? Description { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? MenuId { get; set; }
}