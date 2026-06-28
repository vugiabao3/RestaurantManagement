using System.Collections.Generic;
using MediatR;

namespace RestaurantManagement.Application.MenuItems.Queries.GetMenuItemsByCategory
{
    /// <summary>
    /// UC_KIT_03 - Bước 4: Hiển thị danh sách món ăn thuộc 1 danh mục cụ thể.
    /// Nếu Category để trống -> trả về toàn bộ món ăn.
    /// </summary>
    public class GetMenuItemsByCategoryQuery
        : IRequest<List<MenuItemDto>>
    {
        public string? Category { get; set; }
    }
}
