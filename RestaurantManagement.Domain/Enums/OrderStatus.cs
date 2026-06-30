using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Enums;

public static class OrderStatus
{
    public const string Cart = "Cart";
    public const string Pending = "Pending";
    public const string Preparing = "Preparing";
    public const string Ready = "Ready";
}