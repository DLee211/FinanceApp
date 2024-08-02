using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceApp.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserREDO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Check if the AspNetRoles table exists
            bool tableExists;
            using (var connection = new SqlConnection(migrationBuilder.ActiveProvider))
            {
                connection.Open();
                using (var command = new SqlCommand("IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AspNetRoles') SELECT CAST(1 AS BIT) ELSE SELECT CAST(0 AS BIT)", connection))
                {
                    tableExists = (bool)command.ExecuteScalar();
                }
            }
            
            if (!tableExists)
            {
                migrationBuilder.CreateTable(
                    name: "AspNetRoles",
                    columns: table => new
                    {
                        Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                        Name = table.Column<string>(type: "nvarchar(256)", nullable: true),
                        NormalizedName = table.Column<string>(type: "nvarchar(256)", nullable: true),
                        ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                    });
            }

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
