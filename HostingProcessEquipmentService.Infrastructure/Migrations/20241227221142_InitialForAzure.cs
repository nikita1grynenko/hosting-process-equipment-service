using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HostingProcessEquipmentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialForAzure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProcessEquipments",
                columns: new[] { "Id", "Area", "Code", "Name" },
                values: new object[,]
                {
                    { 1, 25.0, "PE001", "Обладнання 1" },
                    { 2, 30.5, "PE002", "Обладнання 2" },
                    { 3, 15.0, "PE003", "Обладнання 3" }
                });

            migrationBuilder.InsertData(
                table: "ProductionFacilities",
                columns: new[] { "Id", "Code", "Name", "StandardArea" },
                values: new object[,]
                {
                    { 1, "PF001", "Завод 1", 1000.0 },
                    { 2, "PF002", "Завод 2", 1500.0 },
                    { 3, "PF003", "Завод 3", 2000.0 }
                });

            migrationBuilder.InsertData(
                table: "EquipmentPlacementContracts",
                columns: new[] { "Id", "EquipmentQuantity", "ProcessEquipmentId", "ProductionFacilityId" },
                values: new object[,]
                {
                    { 1, 10, 1, 1 },
                    { 2, 5, 2, 1 },
                    { 3, 20, 3, 2 },
                    { 4, 8, 1, 3 },
                    { 5, 12, 2, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EquipmentPlacementContracts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EquipmentPlacementContracts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EquipmentPlacementContracts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EquipmentPlacementContracts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EquipmentPlacementContracts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProcessEquipments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProcessEquipments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProcessEquipments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductionFacilities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductionFacilities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductionFacilities",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
