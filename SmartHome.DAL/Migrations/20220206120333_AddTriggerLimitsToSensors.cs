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
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "upper_trigger_limit",
                table: "sensors",
                type: "real",
                nullable: true);
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
