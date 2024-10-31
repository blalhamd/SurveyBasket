using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Survey.DataAccess.Migrations.AppIdentityDb
{
    /// <inheritdoc />
    public partial class SendUserdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fname", "Lname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "776eba7e-9feb-4597-9193-b118c6911ae8", "blalhamd48@gmail.com", false, "Bilal", "Sayed", false, null, null, null, null, null, false, null, false, "BilalSayed123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
