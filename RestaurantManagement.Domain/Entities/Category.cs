using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Domain.Entities;
namespace RestaurantManagement.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? MenuId { get; set; }
        public Menu? Menu { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        public ICollection<Dish> Dishes
        { get; set; }
        = new List<Dish>();

    }
}
