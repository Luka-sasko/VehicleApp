using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToModelsName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_Name",
                table: "VehicleModels",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMakes_Name",
                table: "VehicleMakes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VehicleModels_Name",
                table: "VehicleModels");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMakes_Name",
                table: "VehicleMakes");
        }
    }
}
