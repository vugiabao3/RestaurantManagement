namespace RestaurantManagement.Application.Users.Queries.GetAllUsers;

public class GetAllUsersResponse
{
    public int UserId { get; set; }

    public string FullName { get; set; } = "";

    public string Email { get; set; } = "";

    public string PhoneNumber { get; set; } = "";

    public string Role { get; set; } = "";
}