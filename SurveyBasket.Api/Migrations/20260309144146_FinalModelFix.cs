using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SurveyBasket.Api.Migrations
{
    /// <inheritdoc />
    public partial class FinalModelFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "C5B32D50-6790-4835-805A-7B2A9AFCA638", "6725ADEA-6A12-453E-B5F5-0570BCCBE2A6", true, false, "Member", "MEMBER" },
                    { "D9832268-91AB-4DA9-B629-22303AE84748", "9130591D-ADE7-4579-AB58-1E115B5B404C", false, false, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "F67E8083-A6CC-4D82-A25E-FC959D103AB7", 0, "82F7C334-9F3A-4EBE-B30C-D6EB3A1C5BD7", "admin@servey-basket.com", true, "Survey Basket", "Admin", false, null, "ADMIN@SERVEY-BASKET.COM", "ADMIN@SERVEY-BASKET.COM", "AQAAAAIAAYagAAAAEOLdLESBbKMIYtQuy/GRevo6VwoMNIsSAG4w18ig0VL/MytFg4l0CenLhnGOH8SM8g==", null, false, "5923A6B3D3FF410EA0A94BA646A3BDE9", false, "admin@servey-basket.com" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permission", "polls:add", "D9832268-91AB-4DA9-B629-22303AE84748" },
                    { 2, "permission", "polls:delete", "D9832268-91AB-4DA9-B629-22303AE84748" },
                    { 3, "permission", "polls:read", "D9832268-91AB-4DA9-B629-22303AE84748" },
                    { 4, "permission", "polls:update", "D9832268-91AB-4DA9-B629-22303AE84748" },
                    { 5, "permission", "questions:add", "D9832268-91AB-4DA9-B629-22303AE84748" },
                    { 6, "permission", "questions:read", "D9832268-91AB-4DA9-B629-22303AE84748" },
                    { 7, "permission", "questions:update", "D9832268-91AB-4DA9-B629-22303AE84748" },
                    { 8, "permission", "results:read", "D9832268-91AB-4DA9-B629-22303AE84748" },
                    { 9, "permission", "roles:add", "D9832268-91AB-4DA9-B629-22303AE84748" },
                    { 10, "permission", "roles:read", "D9832268-91AB-4DA9-B629-22303AE84748" },
                    { 11, "permission", "roles:update", "D9832268-91AB-4DA9-B629-22303AE84748" },
                    { 12, "permission", "users:add", "D9832268-91AB-4DA9-B629-22303AE84748" },
                    { 13, "permission", "users:read", "D9832268-91AB-4DA9-B629-22303AE84748" },
                    { 14, "permission", "users:update", "D9832268-91AB-4DA9-B629-22303AE84748" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "D9832268-91AB-4DA9-B629-22303AE84748", "F67E8083-A6CC-4D82-A25E-FC959D103AB7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "C5B32D50-6790-4835-805A-7B2A9AFCA638");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "D9832268-91AB-4DA9-B629-22303AE84748", "F67E8083-A6CC-4D82-A25E-FC959D103AB7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "D9832268-91AB-4DA9-B629-22303AE84748");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "F67E8083-A6CC-4D82-A25E-FC959D103AB7");
        }
    }
}
