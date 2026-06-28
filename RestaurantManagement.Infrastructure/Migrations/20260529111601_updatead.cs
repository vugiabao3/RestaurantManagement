using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResetPasswordToken",
                table: "ADMIN",
                newName: "ResetPasswordOtp");

            migrationBuilder.RenameColumn(
                name: "ResetPasswordExpiry",
                table: "ADMIN",
                newName: "ResetPasswordOtpExpiry");

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResetPasswordOtpExpiry",
                table: "ADMIN",
                newName: "ResetPasswordExpiry");

            migrationBuilder.RenameColumn(
                name: "ResetPasswordOtp",
                table: "ADMIN",
                newName: "ResetPasswordToken");

           
        }
    }
}
