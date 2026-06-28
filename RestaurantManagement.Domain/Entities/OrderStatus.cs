using System;

namespace RestaurantManagement.Domain.Entities
{
    /// <summary>
    /// Tập trạng thái dùng chung cho Order và OrderDetail
    /// (tương ứng <<enumeration>> OrderStatus trong Class Diagram).
    /// Lưu dưới dạng string (nvarchar) trong CSDL theo đúng ERD.
    /// </summary>
    public static class OrderStatus
    {
        public const string Pending = "PENDING";
        public const string Preparing = "PREPARING";
        public const string Ready = "READY";
        public const string Completed = "COMPLETED";
        public const string OutOfStock = "OUT_OF_STOCK";
        public const string Cancelled = "CANCELLED";
    }
}
