using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartHome.DAL.Migrations
{
    public partial class AddTriggerLimitsToSensors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "lower_trigger_limit",
                table: "sensors",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "upper_trigger_limit",
                table: "sensors",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lower_trigger_limit",
                table: "sensors");

            migrationBuilder.DropColumn(
                name: "upper_trigger_limit",
                table: "sensors");
        }
    }
}
