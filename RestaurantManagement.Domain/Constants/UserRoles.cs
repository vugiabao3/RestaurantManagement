namespace RestaurantManagement.Domain.Constants
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Kitchen = "Kitchen";
        public const string Staff = "Staff";

        public static readonly string[] All = { Admin, Kitchen, Staff };

        public static bool IsValid(string role)
        {
            return All.Contains(role, StringComparer.OrdinalIgnoreCase);
        }
    }
}
