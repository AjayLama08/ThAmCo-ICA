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
                    Lastname = table.Column<string>(type: "TEXT", nullable: false),
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
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
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
                    GuestId = table.Column<int>(type: "INTEGER", nullable: false)
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
                        name: "FK_Staffings_Staffs_EventId",
                        column: x => x.EventId,
                        principalTable: "Staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "DateAndTime", "EventTypeId", "FoodBookingId", "ReservationReference", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "WED", 101, "RES2022063001", "John and Jane's Wedding" },
                    { 2, new DateTime(2022, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "CON", 102, null, "Tech Innovations 2022" },
                    { 3, new DateTime(2022, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "PTY", null, "RES2022081203", "Alice's Birthday Party" },
                    { 4, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "WED", 104, null, "Mike and Emma's Wedding" },
                    { 5, new DateTime(2023, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "CON", null, "RES2023100805", "Digital Marketing Summit" },
                    { 6, new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "PTY", 106, null, "Bob's Surprise Party" },
                    { 7, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "WED", null, null, "Sophie and Adam's Wedding" },
                    { 8, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "MET", 108, "RES2024030508", "AI and Future Tech" },
                    { 9, new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "PTY", null, null, "Frank's 50th Birthday" },
                    { 10, new DateTime(2024, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "MET", 110, null, "Leadership Forum" }
                });

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "GuestId", "Email", "FirstName", "Lastname" },
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
                    { 10, "hank.anderson@example.com", "Hank", "Anderson" }
                });

            migrationBuilder.InsertData(
                table: "Staffs",
                columns: new[] { "StaffId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Sarah", "Jones" },
                    { 2, "Jacob", "Smith" },
                    { 3, "Emily", "Brown" },
                    { 4, "Michael", "Johnson" },
                    { 5, "Jessica", "Williams" },
                    { 6, "David", "Miller" },
                    { 7, "Laura", "Wilson" },
                    { 8, "Daniel", "Taylor" },
                    { 9, "Sophia", "Davis" },
                    { 10, "James", "Anderson" }
                });

            migrationBuilder.InsertData(
                table: "GuestBookings",
                columns: new[] { "EventId", "GuestId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 1, 2 },
                    { 3, 2 },
                    { 3, 3 },
                    { 2, 4 },
                    { 5, 4 },
                    { 4, 5 },
                    { 6, 5 },
                    { 5, 6 },
                    { 6, 7 },
                    { 9, 7 },
                    { 7, 8 },
                    { 7, 9 },
                    { 8, 9 },
                    { 10, 10 }
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
                    { 2, 3 },
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
