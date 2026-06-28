namespace RestaurantManagement.Application.Orders.Commands.UndoOrderDetailStatus
{
    public class UndoOrderDetailStatusResponse
    {
        public string Message { get; set; } = string.Empty;
        public int OrderDetailId { get; set; }
        public string Status { get; set; } = string.Empty;
        public int Version { get; set; }
        public int OrderVersion { get; set; }
        public bool CanCompleteOrder { get; set; }
        public bool RealtimeSynchronized { get; set; }
    }
}
