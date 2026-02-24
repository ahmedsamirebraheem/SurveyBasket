using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyBasket.Api.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddAhmedsamir : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "D765FB08-3390-445C-BF80-07BECDB1F816", 0, "88995738-D0E0-469F-83C3-E9D2849E7C05", "ahmed.samir@test.com", true, "Ahmed", "Samir", false, null, "AHMED.SAMIR@TEST.COM", "AHMED.SAMIR@TEST.COM", "AQAAAAIAAYagAAAAEBZiV4ovFqxy298dGbBJQ0YTX0thl24vwW8ynjggg+MtYZ6E2VCFSt8vyDL90FTBoA==", null, false, "C3688126-B817-4523-9D07-160B8808A613", false, "ahmed.samir@test.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "D765FB08-3390-445C-BF80-07BECDB1F816");
        }
    }
}
