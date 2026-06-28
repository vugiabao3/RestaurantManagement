using System.Collections.Generic;
using MediatR;

namespace RestaurantManagement.Application.MenuItems.Queries.GetMenuCategories
{
    /// <summary>
    /// UC_KIT_03 - Bước 2: Hiển thị các danh mục thực đơn
    /// (Sáng, Trưa, Tối, Tiệc...) kèm số lượng món mỗi danh mục.
    /// </summary>
    public class GetMenuCategoriesQuery
        : IRequest<List<MenuCategorySummaryDto>>
    {
    }
}
