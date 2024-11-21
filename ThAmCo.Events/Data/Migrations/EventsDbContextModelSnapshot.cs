﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThAmCo.Events.Data;

#nullable disable

namespace ThAmCo.Events.Data.Migrations
{
    [DbContext(typeof(EventsDbContext))]
    partial class EventsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("ThAmCo.Events.Data.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("EventTypeId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("FoodBookingId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReservationReference")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("EventId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            EventId = 1,
                            DateAndTime = new DateTime(2022, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventTypeId = "WED",
                            FoodBookingId = 101,
                            ReservationReference = "RES2022063001",
                            Title = "John and Jane's Wedding"
                        },
                        new
                        {
                            EventId = 2,
                            DateAndTime = new DateTime(2022, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventTypeId = "CON",
                            FoodBookingId = 102,
                            Title = "Tech Innovations 2022"
                        },
                        new
                        {
                            EventId = 3,
                            DateAndTime = new DateTime(2022, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventTypeId = "PTY",
                            ReservationReference = "RES2022081203",
                            Title = "Alice's Birthday Party"
                        },
                        new
                        {
                            EventId = 4,
                            DateAndTime = new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventTypeId = "WED",
                            FoodBookingId = 104,
                            Title = "Mike and Emma's Wedding"
                        },
                        new
                        {
                            EventId = 5,
                            DateAndTime = new DateTime(2023, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventTypeId = "CON",
                            ReservationReference = "RES2023100805",
                            Title = "Digital Marketing Summit"
                        },
                        new
                        {
                            EventId = 6,
                            DateAndTime = new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventTypeId = "PTY",
                            FoodBookingId = 106,
                            Title = "Bob's Surprise Party"
                        },
                        new
                        {
                            EventId = 7,
                            DateAndTime = new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventTypeId = "WED",
                            Title = "Sophie and Adam's Wedding"
                        },
                        new
                        {
                            EventId = 8,
                            DateAndTime = new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventTypeId = "MET",
                            FoodBookingId = 108,
                            ReservationReference = "RES2024030508",
                            Title = "AI and Future Tech"
                        },
                        new
                        {
                            EventId = 9,
                            DateAndTime = new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventTypeId = "PTY",
                            Title = "Frank's 50th Birthday"
                        },
                        new
                        {
                            EventId = 10,
                            DateAndTime = new DateTime(2024, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventTypeId = "MET",
                            FoodBookingId = 110,
                            Title = "Leadership Forum"
                        });
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Guest", b =>
                {
                    b.Property<int>("GuestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("GuestId");

                    b.ToTable("Guests");

                    b.HasData(
                        new
                        {
                            GuestId = 1,
                            Email = "john.doe@example.com",
                            FirstName = "John",
                            LastName = "Doe"
                        },
                        new
                        {
                            GuestId = 2,
                            Email = "jane.smith@example.com",
                            FirstName = "Jane",
                            LastName = "Smith"
                        },
                        new
                        {
                            GuestId = 3,
                            Email = "alice.johnson@example.com",
                            FirstName = "Alice",
                            LastName = "Johnson"
                        },
                        new
                        {
                            GuestId = 4,
                            Email = "bob.brown@example.com",
                            FirstName = "Bob",
                            LastName = "Brown"
                        },
                        new
                        {
                            GuestId = 5,
                            Email = "charlie.davis@example.com",
                            FirstName = "Charlie",
                            LastName = "Davis"
                        },
                        new
                        {
                            GuestId = 6,
                            Email = "david.martinez@example.com",
                            FirstName = "David",
                            LastName = "Martinez"
                        },
                        new
                        {
                            GuestId = 7,
                            Email = "eve.miller@example.com",
                            FirstName = "Eve",
                            LastName = "Miller"
                        },
                        new
                        {
                            GuestId = 8,
                            Email = "frank.wilson@example.com",
                            FirstName = "Frank",
                            LastName = "Wilson"
                        },
                        new
                        {
                            GuestId = 9,
                            Email = "grace.taylor@example.com",
                            FirstName = "Grace",
                            LastName = "Taylor"
                        },
                        new
                        {
                            GuestId = 10,
                            Email = "hank.anderson@example.com",
                            FirstName = "Hank",
                            LastName = "Anderson"
                        });
                });

            modelBuilder.Entity("ThAmCo.Events.Data.GuestBooking", b =>
                {
                    b.Property<int>("GuestId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasAttended")
                        .HasColumnType("INTEGER");

                    b.HasKey("GuestId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("GuestBookings");

                    b.HasData(
                        new
                        {
                            GuestId = 1,
                            EventId = 1,
                            HasAttended = false
                        },
                        new
                        {
                            GuestId = 1,
                            EventId = 2,
                            HasAttended = false
                        },
                        new
                        {
                            GuestId = 2,
                            EventId = 1,
                            HasAttended = false
                        },
                        new
                        {
                            GuestId = 2,
                            EventId = 3,
                            HasAttended = false
                        },
                        new
                        {
                            GuestId = 3,
                            EventId = 3,
                            HasAttended = false
                        },
                        new
                        {
                            GuestId = 4,
                            EventId = 2,
                            HasAttended = false
                        },
                        new
                        {
                            GuestId = 4,
                            EventId = 5,
                            HasAttended = false
                        },
                        new
                        {
                            GuestId = 5,
                            EventId = 4,
                            HasAttended = false
                        },
                        new
                        {
                            GuestId = 5,
                            EventId = 6,
                            HasAttended = false
                        },
                        new
                        {
                            GuestId = 6,
                            EventId = 5,
                            HasAttended = false
                        },
                        new
                        {
                            GuestId = 7,
                            EventId = 6,
                            HasAttended = false
                        },
                        new
                        {
                            GuestId = 7,
                            EventId = 9,
                            HasAttended = false
                        },
                        new
                        {
                            GuestId = 8,
                            EventId = 7,
                            HasAttended = false
                        },
                        new
                        {
                            GuestId = 9,
                            EventId = 7,
                            HasAttended = false
                        },
                        new
                        {
                            GuestId = 9,
                            EventId = 8,
                            HasAttended = false
                        },
                        new
                        {
                            GuestId = 10,
                            EventId = 10,
                            HasAttended = false
                        });
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Staff", b =>
                {
                    b.Property<int>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("StaffId");

                    b.ToTable("Staffs");

                    b.HasData(
                        new
                        {
                            StaffId = 1,
                            FirstName = "Sarah",
                            LastName = "Jones"
                        },
                        new
                        {
                            StaffId = 2,
                            FirstName = "Jacob",
                            LastName = "Smith"
                        },
                        new
                        {
                            StaffId = 3,
                            FirstName = "Emily",
                            LastName = "Brown"
                        },
                        new
                        {
                            StaffId = 4,
                            FirstName = "Michael",
                            LastName = "Johnson"
                        },
                        new
                        {
                            StaffId = 5,
                            FirstName = "Jessica",
                            LastName = "Williams"
                        },
                        new
                        {
                            StaffId = 6,
                            FirstName = "David",
                            LastName = "Miller"
                        },
                        new
                        {
                            StaffId = 7,
                            FirstName = "Laura",
                            LastName = "Wilson"
                        },
                        new
                        {
                            StaffId = 8,
                            FirstName = "Daniel",
                            LastName = "Taylor"
                        },
                        new
                        {
                            StaffId = 9,
                            FirstName = "Sophia",
                            LastName = "Davis"
                        },
                        new
                        {
                            StaffId = 10,
                            FirstName = "James",
                            LastName = "Anderson"
                        });
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Staffing", b =>
                {
                    b.Property<int>("StaffId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.HasKey("StaffId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("Staffings");

                    b.HasData(
                        new
                        {
                            StaffId = 1,
                            EventId = 1
                        },
                        new
                        {
                            StaffId = 2,
                            EventId = 1
                        },
                        new
                        {
                            StaffId = 3,
                            EventId = 2
                        },
                        new
                        {
                            StaffId = 4,
                            EventId = 2
                        },
                        new
                        {
                            StaffId = 5,
                            EventId = 3
                        },
                        new
                        {
                            StaffId = 6,
                            EventId = 3
                        },
                        new
                        {
                            StaffId = 7,
                            EventId = 4
                        },
                        new
                        {
                            StaffId = 8,
                            EventId = 4
                        },
                        new
                        {
                            StaffId = 9,
                            EventId = 5
                        },
                        new
                        {
                            StaffId = 10,
                            EventId = 5
                        },
                        new
                        {
                            StaffId = 1,
                            EventId = 6
                        },
                        new
                        {
                            StaffId = 2,
                            EventId = 6
                        },
                        new
                        {
                            StaffId = 3,
                            EventId = 7
                        },
                        new
                        {
                            StaffId = 4,
                            EventId = 7
                        },
                        new
                        {
                            StaffId = 5,
                            EventId = 8
                        },
                        new
                        {
                            StaffId = 6,
                            EventId = 8
                        },
                        new
                        {
                            StaffId = 7,
                            EventId = 9
                        },
                        new
                        {
                            StaffId = 8,
                            EventId = 9
                        },
                        new
                        {
                            StaffId = 9,
                            EventId = 10
                        },
                        new
                        {
                            StaffId = 10,
                            EventId = 10
                        });
                });

            modelBuilder.Entity("ThAmCo.Events.Data.GuestBooking", b =>
                {
                    b.HasOne("ThAmCo.Events.Data.Event", "Event")
                        .WithMany("GuestBookings")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThAmCo.Events.Data.Guest", "Guest")
                        .WithMany("GuestBookings")
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Guest");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Staffing", b =>
                {
                    b.HasOne("ThAmCo.Events.Data.Event", "Event")
                        .WithMany("Staffings")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThAmCo.Events.Data.Staff", "Staff")
                        .WithMany("Staffings")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Event", b =>
                {
                    b.Navigation("GuestBookings");

                    b.Navigation("Staffings");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Guest", b =>
                {
                    b.Navigation("GuestBookings");
                });

            modelBuilder.Entity("ThAmCo.Events.Data.Staff", b =>
                {
                    b.Navigation("Staffings");
                });
#pragma warning restore 612, 618
        }
    }
}
