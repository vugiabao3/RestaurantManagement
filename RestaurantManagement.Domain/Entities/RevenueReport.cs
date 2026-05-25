using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RestaurantManagement.Domain.Entities
{
    public class RevenueReport : Report
    {
        public int TotalOrders { get; set; }

        public decimal TotalRevenue { get; set; }
    }

}
