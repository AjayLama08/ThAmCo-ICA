using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThAmCo.Catering.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    FoodItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    UnitPrice = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.FoodItemId);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MenuName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "FoodBookings",
                columns: table => new
                {
                    FoodBookingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientReferenceId = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodBookings", x => x.FoodBookingId);
                    table.ForeignKey(
                        name: "FK_FoodBookings_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuFoodItems",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false),
                    FoodItemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuFoodItems", x => new { x.MenuId, x.FoodItemId });
                    table.ForeignKey(
                        name: "FK_MenuFoodItems_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "FoodItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuFoodItems_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FoodItems",
                columns: new[] { "FoodItemId", "Description", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Spicy Chicken Curry with Coconut Milk", 10.5f },
                    { 2, "Steamed Jasmine Rice", 5f },
                    { 3, "Hard-Boiled Free-Range Eggs", 3.5f },
                    { 4, "Grilled Salmon with Lemon Butter Sauce", 18.75f },
                    { 5, "Caesar Salad with Grilled Chicken", 12f },
                    { 6, "Margherita Pizza with Fresh Basil", 15f },
                    { 7, "Beef Burger with Caramelized Onions", 14.25f },
                    { 8, "Vegan Quinoa Salad with Avocado", 11.5f },
                    { 9, "Creamy Mushroom Risotto", 16.3f },
                    { 10, "BBQ Ribs with Coleslaw", 20f },
                    { 11, "Chocolate Lava Cake with Vanilla Ice Cream", 8.75f },
                    { 12, "Garlic Butter Shrimp Skewers", 19.9f },
                    { 13, "Vegetable Stir-Fry with Tofu", 9.4f },
                    { 14, "Penne Alfredo with Grilled Chicken", 13.6f },
                    { 15, "Fresh Fruit Platter with Seasonal Berries", 7.2f }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "MenuId", "MenuName" },
                values: new object[,]
                {
                    { 1, "Vegetarian" },
                    { 2, "Non-Vegetarian" },
                    { 3, "Vegan" },
                    { 4, "Gluten-Free" },
                    { 5, "Pescatarian" },
                    { 6, "Keto" },
                    { 7, "Paleo" },
                    { 8, "Low-Carb" },
                    { 9, "High-Protein" },
                    { 10, "Dairy-Free" },
                    { 11, "Nut-Free" },
                    { 12, "Halal" },
                    { 13, "Kosher" },
                    { 14, "Organic" },
                    { 15, "Mediterranean" }
                });

            migrationBuilder.InsertData(
                table: "FoodBookings",
                columns: new[] { "FoodBookingId", "ClientReferenceId", "EventId", "MenuId", "NumberOfGuests" },
                values: new object[,]
                {
                    { 1, 101, null, 1, 10 },
                    { 2, 102, null, 2, 20 },
                    { 3, 103, null, 3, 30 },
                    { 4, 104, null, 4, 15 },
                    { 5, 105, null, 5, 25 },
                    { 6, 106, null, 6, 35 },
                    { 7, 107, null, 7, 40 },
                    { 8, 108, null, 8, 12 },
                    { 9, 109, null, 9, 18 },
                    { 10, 110, null, 10, 22 },
                    { 11, 111, null, 11, 28 },
                    { 12, 112, null, 12, 32 },
                    { 13, 113, null, 13, 45 },
                    { 14, 114, null, 14, 50 },
                    { 15, 115, null, 15, 60 }
                });

            migrationBuilder.InsertData(
                table: "MenuFoodItems",
                columns: new[] { "FoodItemId", "MenuId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 3 },
                    { 8, 3 },
                    { 7, 4 },
                    { 12, 5 },
                    { 9, 6 },
                    { 13, 7 },
                    { 11, 8 },
                    { 14, 9 },
                    { 6, 10 },
                    { 10, 11 },
                    { 15, 12 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodBookings_MenuId",
                table: "FoodBookings",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuFoodItems_FoodItemId",
                table: "MenuFoodItems",
                column: "FoodItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodBookings");

            migrationBuilder.DropTable(
                name: "MenuFoodItems");

            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
