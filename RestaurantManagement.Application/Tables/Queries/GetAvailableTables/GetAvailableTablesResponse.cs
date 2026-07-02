namespace RestaurantManagement.Application.Tables.Queries.GetAvailableTables;

public class GetAvailableTablesResponse
{
    public int TableId { get; set; }

    public string TableNumber { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public string CurrentStatus { get; set; } = string.Empty;
}