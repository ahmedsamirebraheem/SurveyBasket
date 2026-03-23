using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SurveyBasket.Api.Migrations
{
    /// <inheritdoc />
    public partial class updateIdentityDefaultDataAndDeleteTheEntireDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleId",
                value: "019d165a-c031-7c51-b101-fa31b79e7adf");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoleId",
                value: "019d165a-c031-7c51-b101-fa31b79e7adf");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoleId",
                value: "019d165a-c031-7c51-b101-fa31b79e7adf");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "RoleId",
                value: "019d165a-c031-7c51-b101-fa31b79e7adf");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5,
                column: "RoleId",
                value: "019d165a-c031-7c51-b101-fa31b79e7adf");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6,
                column: "RoleId",
                value: "019d165a-c031-7c51-b101-fa31b79e7adf");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7,
                column: "RoleId",
                value: "019d165a-c031-7c51-b101-fa31b79e7adf");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8,
                column: "RoleId",
                value: "019d165a-c031-7c51-b101-fa31b79e7adf");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9,
                column: "RoleId",
                value: "019d165a-c031-7c51-b101-fa31b79e7adf");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10,
                column: "RoleId",
                value: "019d165a-c031-7c51-b101-fa31b79e7adf");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11,
                column: "RoleId",
                value: "019d165a-c031-7c51-b101-fa31b79e7adf");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12,
                column: "RoleId",
                value: "019d165a-c031-7c51-b101-fa31b79e7adf");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13,
                column: "RoleId",
                value: "019d165a-c031-7c51-b101-fa31b79e7adf");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14,
                column: "RoleId",
                value: "019d165a-c031-7c51-b101-fa31b79e7adf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "019d165a-c031-7c51-b101-fa31b79e7adf", "019d165a-c031-79dd-acd7-bc37bc6e8d6c", false, false, "Admin", "ADMIN" },
                    { "019d165a-c031-7dd0-86a2-f0a4248e6e13", "019d165a-c031-71ae-af48-6ce7c32ed161", true, false, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDisabled", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "019d165a-c031-70a2-ab84-34643272ef15", 0, "019d165a-c031-7838-950f-e263f082cf99", "admin@servey-basket.com", true, "Survey Basket", false, "Admin", false, null, "ADMIN@SERVEY-BASKET.COM", "ADMIN@SERVEY-BASKET.COM", "AQAAAAIAAYagAAAAEDAQzZBJSDmRHWBQEnwTkUW6IVADUlCGA0R1aD8MvEiSdnCDZLKZvlvvOYBDVg1nLg==", null, false, "5923A6B3D3FF410EA0A94BA646A3BDE9", false, "admin@servey-basket.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "019d165a-c031-7c51-b101-fa31b79e7adf", "019d165a-c031-70a2-ab84-34643272ef15" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019d165a-c031-7dd0-86a2-f0a4248e6e13");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "019d165a-c031-7c51-b101-fa31b79e7adf", "019d165a-c031-70a2-ab84-34643272ef15" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019d165a-c031-7c51-b101-fa31b79e7adf");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019d165a-c031-70a2-ab84-34643272ef15");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleId",
                value: "D9832268-91AB-4DA9-B629-22303AE84748");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoleId",
                value: "D9832268-91AB-4DA9-B629-22303AE84748");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoleId",
                value: "D9832268-91AB-4DA9-B629-22303AE84748");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "RoleId",
                value: "D9832268-91AB-4DA9-B629-22303AE84748");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5,
                column: "RoleId",
                value: "D9832268-91AB-4DA9-B629-22303AE84748");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6,
                column: "RoleId",
                value: "D9832268-91AB-4DA9-B629-22303AE84748");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7,
                column: "RoleId",
                value: "D9832268-91AB-4DA9-B629-22303AE84748");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8,
                column: "RoleId",
                value: "D9832268-91AB-4DA9-B629-22303AE84748");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9,
                column: "RoleId",
                value: "D9832268-91AB-4DA9-B629-22303AE84748");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10,
                column: "RoleId",
                value: "D9832268-91AB-4DA9-B629-22303AE84748");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11,
                column: "RoleId",
                value: "D9832268-91AB-4DA9-B629-22303AE84748");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12,
                column: "RoleId",
                value: "D9832268-91AB-4DA9-B629-22303AE84748");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13,
                column: "RoleId",
                value: "D9832268-91AB-4DA9-B629-22303AE84748");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14,
                column: "RoleId",
                value: "D9832268-91AB-4DA9-B629-22303AE84748");

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
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IsDisabled", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "F67E8083-A6CC-4D82-A25E-FC959D103AB7", 0, "82F7C334-9F3A-4EBE-B30C-D6EB3A1C5BD7", "admin@servey-basket.com", true, "Survey Basket", false, "Admin", false, null, "ADMIN@SERVEY-BASKET.COM", "ADMIN@SERVEY-BASKET.COM", "AQAAAAIAAYagAAAAEDAQzZBJSDmRHWBQEnwTkUW6IVADUlCGA0R1aD8MvEiSdnCDZLKZvlvvOYBDVg1nLg==", null, false, "5923A6B3D3FF410EA0A94BA646A3BDE9", false, "admin@servey-basket.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "D9832268-91AB-4DA9-B629-22303AE84748", "F67E8083-A6CC-4D82-A25E-FC959D103AB7" });
        }
    }
}
