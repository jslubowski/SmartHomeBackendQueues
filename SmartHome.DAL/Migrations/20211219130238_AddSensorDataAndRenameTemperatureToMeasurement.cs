using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SmartHome.DAL.Migrations
{
    public partial class AddSensorDataAndRenameTemperatureToMeasurement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "temperature_variant");

            migrationBuilder.DropTable(
                name: "temperatures");

            migrationBuilder.CreateTable(
                name: "measurement_type_variant",
                columns: table => new
                {
                    measurement_type = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_measurement_type_variant", x => x.measurement_type);
                });

            migrationBuilder.CreateTable(
                name: "measurement_unit_variant",
                columns: table => new
                {
                    measurement_unit = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_measurement_unit_variant", x => x.measurement_unit);
                });

            migrationBuilder.CreateTable(
                name: "sensors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    measurement_type = table.Column<int>(type: "integer", nullable: false),
                    measurement_unit = table.Column<int>(type: "integer", nullable: false),
                    custom_name = table.Column<string>(type: "text", nullable: true),
                    latest_value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sensors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "measurement",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    sensor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_measurement", x => x.id);
                    table.ForeignKey(
                        name: "fk_measurement_sensors_sensor_id",
                        column: x => x.sensor_id,
                        principalTable: "sensors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "measurement_type_variant",
                columns: new[] { "measurement_type", "name" },
                values: new object[,]
                {
                    { 1, "Temperature" },
                    { 2, "Humidity" }
                });

            migrationBuilder.InsertData(
                table: "measurement_unit_variant",
                columns: new[] { "measurement_unit", "name" },
                values: new object[,]
                {
                    { 1, "Celsius" },
                    { 2, "Fahrenheit" },
                    { 3, "Kelvin" },
                    { 4, "RH" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_measurement_sensor_id",
                table: "measurement",
                column: "sensor_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "measurement");

            migrationBuilder.DropTable(
                name: "measurement_type_variant");

            migrationBuilder.DropTable(
                name: "measurement_unit_variant");

            migrationBuilder.DropTable(
                name: "sensors");

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
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    sensor_name = table.Column<string>(type: "text", nullable: true),
                    temperature_unit = table.Column<int>(type: "integer", nullable: false),
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
    }
}
