using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SmartHome.DAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "temperature_variant",
                columns: table => new
                {
                    temperature_unit = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_temperature_variant", x => x.temperature_unit);
                });

            migrationBuilder.CreateTable(
                name: "temperatures",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sensor_name = table.Column<string>(type: "text", nullable: true),
                    temperature_unit = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_temperatures", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "temperature_variant",
                columns: new[] { "temperature_unit", "name" },
                values: new object[,]
                {
                    { 1, "Celsius" },
                    { 2, "Fahrenheit" },
                    { 3, "Kelvin" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "temperature_variant");

            migrationBuilder.DropTable(
                name: "temperatures");
        }
    }
}
