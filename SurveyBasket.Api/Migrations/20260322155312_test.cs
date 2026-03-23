using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyBasket.Api.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "F67E8083-A6CC-4D82-A25E-FC959D103AB7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDAQzZBJSDmRHWBQEnwTkUW6IVADUlCGA0R1aD8MvEiSdnCDZLKZvlvvOYBDVg1nLg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "F67E8083-A6CC-4D82-A25E-FC959D103AB7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOLdLESBbKMIYtQuy/GRevo6VwoMNIsSAG4w18ig0VL/MytFg4l0CenLhnGOH8SM8g==");
        }
    }
}
