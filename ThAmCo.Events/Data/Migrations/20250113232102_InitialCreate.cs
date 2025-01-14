using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThAmCo.Events.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventTypeId = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    DateAndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    FoodBookingId = table.Column<int>(type: "INTEGER", nullable: true),
                    ReservationReference = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    GuestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.GuestId);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    IsFirstAider = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "GuestBookings",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "INTEGER", nullable: false),
                    GuestId = table.Column<int>(type: "INTEGER", nullable: false),
                    HasAttended = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestBookings", x => new { x.GuestId, x.EventId });
                    table.ForeignKey(
                        name: "FK_GuestBookings_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuestBookings_Guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "GuestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staffings",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffings", x => new { x.StaffId, x.EventId });
                    table.ForeignKey(
                        name: "FK_Staffings_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Staffings_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "DateAndTime", "EventTypeId", "FoodBookingId", "IsDeleted", "ReservationReference", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "WED", 101, false, null, "John and Jane's Wedding" },
                    { 2, new DateTime(2022, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "CON", 102, false, null, "Tech Innovations 2022" },
                    { 3, new DateTime(2022, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "PTY", null, false, null, "Alice's Birthday Party" },
                    { 4, new DateTime(2022, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "WED", 104, false, null, "Mike and Emma's Wedding" },
                    { 5, new DateTime(2022, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "CON", null, false, null, "Digital Marketing Summit" },
                    { 6, new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "PTY", 106, false, null, "Bob's Surprise Party" },
                    { 7, new DateTime(2023, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "WED", null, false, null, "Sophie and Adam's Wedding" },
                    { 8, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "MET", 108, false, null, "AI and Future Tech" },
                    { 9, new DateTime(2023, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "PTY", null, false, null, "Frank's 50th Birthday" },
                    { 10, new DateTime(2023, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "MET", 110, false, null, "Leadership Forum" }
                });

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "GuestId", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John", "Doe" },
                    { 2, "jane.smith@example.com", "Jane", "Smith" },
                    { 3, "alice.johnson@example.com", "Alice", "Johnson" },
                    { 4, "bob.brown@example.com", "Bob", "Brown" },
                    { 5, "charlie.davis@example.com", "Charlie", "Davis" },
                    { 6, "david.martinez@example.com", "David", "Martinez" },
                    { 7, "eve.miller@example.com", "Eve", "Miller" },
                    { 8, "frank.wilson@example.com", "Frank", "Wilson" },
                    { 9, "grace.taylor@example.com", "Grace", "Taylor" },
                    { 10, "hank.anderson@example.com", "Hank", "Anderson" },
                    { 11, "isla.scott@example.com", "Isla", "Scott" },
                    { 12, "jack.evans@example.com", "Jack", "Evans" },
                    { 13, "karen.hughes@example.com", "Karen", "Hughes" },
                    { 14, "liam.white@example.com", "Liam", "White" },
                    { 15, "mia.young@example.com", "Mia", "Young" },
                    { 16, "noah.hall@example.com", "Noah", "Hall" },
                    { 17, "olivia.king@example.com", "Olivia", "King" },
                    { 18, "paul.wright@example.com", "Paul", "Wright" },
                    { 19, "quinn.harris@example.com", "Quinn", "Harris" },
                    { 20, "rachel.lewis@example.com", "Rachel", "Lewis" },
                    { 21, "samuel.robinson@example.com", "Samuel", "Robinson" },
                    { 22, "tina.walker@example.com", "Tina", "Walker" },
                    { 23, "victor.green@example.com", "Victor", "Green" }
                });

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "StaffId", "FirstName", "IsFirstAider", "LastName" },
                values: new object[,]
                {
                    { 1, "Sarah", true, "Jones" },
                    { 2, "Jacob", false, "Smith" },
                    { 3, "Emily", true, "Brown" },
                    { 4, "Michael", false, "Johnson" },
                    { 5, "Jessica", true, "Williams" },
                    { 6, "David", false, "Miller" },
                    { 7, "Laura", false, "Wilson" },
                    { 8, "Daniel", false, "Taylor" },
                    { 9, "Sophia", true, "Davis" },
                    { 10, "James", false, "Anderson" }
                });

            migrationBuilder.InsertData(
                table: "GuestBookings",
                columns: new[] { "EventId", "GuestId", "HasAttended" },
                values: new object[,]
                {
                    { 1, 1, false },
                    { 2, 1, false },
                    { 1, 2, false },
                    { 2, 2, false },
                    { 3, 2, false },
                    { 2, 3, false },
                    { 3, 3, false },
                    { 5, 3, false },
                    { 2, 4, false },
                    { 6, 4, false },
                    { 2, 5, false },
                    { 4, 5, false },
                    { 6, 5, false },
                    { 2, 6, false },
                    { 5, 6, false },
                    { 7, 6, false },
                    { 2, 7, false },
                    { 6, 7, false },
                    { 9, 7, false },
                    { 2, 8, false },
                    { 7, 8, false },
                    { 8, 8, false },
                    { 2, 9, false },
                    { 7, 9, false },
                    { 10, 9, false },
                    { 2, 10, false },
                    { 10, 10, false },
                    { 2, 11, false },
                    { 8, 11, false },
                    { 9, 11, false },
                    { 4, 12, false },
                    { 10, 12, false },
                    { 3, 13, false },
                    { 6, 13, false },
                    { 7, 14, false },
                    { 8, 14, false },
                    { 2, 15, false },
                    { 9, 15, false },
                    { 5, 16, false },
                    { 10, 16, false },
                    { 1, 17, false },
                    { 7, 17, false },
                    { 6, 18, false },
                    { 8, 18, false },
                    { 4, 19, false },
                    { 9, 19, false },
                    { 2, 20, false },
                    { 8, 20, false },
                    { 5, 21, false },
                    { 10, 21, false },
                    { 3, 22, false },
                    { 7, 22, false },
                    { 6, 23, false },
                    { 9, 23, false }
                });

            migrationBuilder.InsertData(
                table: "Staffings",
                columns: new[] { "EventId", "StaffId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 6, 1 },
                    { 1, 2 },
                    { 6, 2 },
                    { 7, 3 },
                    { 2, 4 },
                    { 7, 4 },
                    { 3, 5 },
                    { 8, 5 },
                    { 3, 6 },
                    { 8, 6 },
                    { 4, 7 },
                    { 9, 7 },
                    { 4, 8 },
                    { 9, 8 },
                    { 5, 9 },
                    { 10, 9 },
                    { 5, 10 },
                    { 10, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuestBookings_EventId",
                table: "GuestBookings",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Staffings_EventId",
                table: "Staffings",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuestBookings");

            migrationBuilder.DropTable(
                name: "Staffings");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Staffs");
        }
    }
}
