namespace RestaurantManagement.Domain.Constants.Tracking;

public static class FoodStatusConstants
{
    public const string Pending = "PENDING";
    public const string Cooking = "COOKING";
    public const string Done = "DONE";
    public const string Cancelled = "CANCELLED";

    public static readonly IReadOnlyDictionary<string, string[]> ValidTransitions =
        new Dictionary<string, string[]>
        {
            [Pending] = new[] { Cooking, Cancelled },
            [Cooking] = new[] { Done, Cancelled },
            [Done] = Array.Empty<string>(),
            [Cancelled] = Array.Empty<string>()
        };

    public static bool CanChange(string currentStatus, string nextStatus)
        => ValidTransitions.TryGetValue(currentStatus, out var nextStatuses)
           && nextStatuses.Contains(nextStatus);
}
