using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application
    .Dishes.Queries.SearchDishes;

public class SearchDishesResponse
{
    public int DishId { get; set; }

    public string Name { get; set; }
        = string.Empty;

    public decimal Price { get; set; }
}