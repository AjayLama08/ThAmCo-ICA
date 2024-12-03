﻿using Microsoft.EntityFrameworkCore;
namespace ThAmCo.Events.Data
{
    public class EventsDbContext : DbContext
    {
        //DbSets which represent the tables in the database
        public DbSet<Event> Events { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<GuestBooking> GuestBookings { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Staffing> Staffings { get; set; }

        //Path to the database file
        public string DbPath { get; set; } = string.Empty;

        //Constructor to set the path to the database file
        public EventsDbContext()
        {
            var folder = Environment.SpecialFolder.MyDocuments;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "ThAmCo.Events.db");
        }

        //Method to configure the database connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data source = {DbPath}");
        }

        //Method to configure the database model
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Foreign key constraints
            builder.Entity<GuestBooking>()
                .HasKey(gb => new { gb.GuestId, gb.EventId });

            //Foreign key constraints
            builder.Entity<Staffing>()
                .HasKey(s => new { s.StaffId, s.EventId });

            //Navigation properties
            builder.Entity<GuestBooking>()
                .HasOne(g => g.Guest) 
                .WithMany(gb => gb.GuestBookings)
                .HasForeignKey(g => g.GuestId);

            //Navigation properties
            builder.Entity<GuestBooking>()
                .HasOne(e => e.Event)
                .WithMany(gb => gb.GuestBookings)
                .HasForeignKey(e => e.EventId);

            //Navigation properties
            builder.Entity<Staffing>()
                .HasOne(s => s.Staff)
                .WithMany(s => s.Staffings)
                .HasForeignKey(s => s.EventId);

            //Navigation properties
            builder.Entity<Staffing>()
                .HasOne(e => e.Event)
                .WithMany(s => s.Staffings)
                .HasForeignKey(e => e.EventId);

            //Seed data to populate Guest Table
            builder.Entity<Guest>().HasData(
             new Guest { GuestId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
             new Guest { GuestId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" },
             new Guest { GuestId = 3, FirstName = "Alice", LastName = "Johnson", Email = "alice.johnson@example.com" },
             new Guest { GuestId = 4, FirstName = "Bob", LastName = "Brown", Email = "bob.brown@example.com" },
             new Guest { GuestId = 5, FirstName = "Charlie", LastName = "Davis", Email = "charlie.davis@example.com" },
             new Guest { GuestId = 6, FirstName = "David", LastName = "Martinez", Email = "david.martinez@example.com" },
             new Guest { GuestId = 7, FirstName = "Eve", LastName = "Miller", Email = "eve.miller@example.com" },
             new Guest { GuestId = 8, FirstName = "Frank", LastName = "Wilson", Email = "frank.wilson@example.com" },
             new Guest { GuestId = 9, FirstName = "Grace", LastName = "Taylor", Email = "grace.taylor@example.com" },
             new Guest { GuestId = 10, FirstName = "Hank", LastName = "Anderson", Email = "hank.anderson@example.com"
             });

            //Seed data to populate Event Table
            builder.Entity<Event>().HasData(
             new Event { EventId = 1, EventTypeId = "WED", Title = "John and Jane's Wedding", DateAndTime = new DateTime(2022, 11, 30), FoodBookingId = 101, ReservationReference = "RES2022063001" },
             new Event { EventId = 2, EventTypeId = "CON", Title = "Tech Innovations 2022", DateAndTime = new DateTime(2022, 11, 15), FoodBookingId = 102, ReservationReference = null },
             new Event { EventId = 3, EventTypeId = "PTY", Title = "Alice's Birthday Party", DateAndTime = new DateTime(2022, 11, 12), FoodBookingId = null, ReservationReference = "RES2022081203" },
             new Event { EventId = 4, EventTypeId = "WED", Title = "Mike and Emma's Wedding", DateAndTime = new DateTime(2022, 11, 20), FoodBookingId = 104, ReservationReference = null },
             new Event { EventId = 5, EventTypeId = "CON", Title = "Digital Marketing Summit", DateAndTime = new DateTime(2022, 11, 8), FoodBookingId = null, ReservationReference = "RES2023100805" },
             new Event { EventId = 6, EventTypeId = "PTY", Title = "Bob's Surprise Party", DateAndTime = new DateTime(2023, 1, 5), FoodBookingId = 106, ReservationReference = null },
             new Event { EventId = 7, EventTypeId = "WED", Title = "Sophie and Adam's Wedding", DateAndTime = new DateTime(2023, 1, 13), FoodBookingId = null, ReservationReference = null },
             new Event { EventId = 8, EventTypeId = "MET", Title = "AI and Future Tech", DateAndTime = new DateTime(2023, 1, 15), FoodBookingId = 108, ReservationReference = "RES2024030508" },
             new Event { EventId = 9, EventTypeId = "PTY", Title = "Frank's 50th Birthday", DateAndTime = new DateTime(2023, 1, 17), FoodBookingId = null, ReservationReference = null },
             new Event { EventId = 10, EventTypeId = "MET", Title = "Leadership Forum", DateAndTime = new DateTime(2023, 1, 22), FoodBookingId = 110, ReservationReference = null }
);

            //Seed data to populate GuestBooking Table
            builder.Entity<GuestBooking>().HasData(
             // Guest 1 (John Doe)
             new GuestBooking { GuestId = 1, EventId = 1 },  // John at John & Jane's Wedding
             new GuestBooking { GuestId = 1, EventId = 2 },  // John at Tech Innovations 2022

             // Guest 2 (Jane Smith)
             new GuestBooking { GuestId = 2, EventId = 1 },  // Jane at John & Jane's Wedding
             new GuestBooking { GuestId = 2, EventId = 3 },  // Jane at Alice's Birthday Party

             // Guest 3 (Alice Johnson)
             new GuestBooking { GuestId = 3, EventId = 3 },  // Alice at Alice's Birthday Party

             // Guest 4 (Bob Brown)
             new GuestBooking { GuestId = 4, EventId = 2 },  // Bob at Tech Innovations 2022
             new GuestBooking { GuestId = 4, EventId = 5 },  // Bob at Digital Marketing Summit

             // Guest 5 (Charlie Davis)
             new GuestBooking { GuestId = 5, EventId = 4 },  // Charlie at Mike & Emma's Wedding
             new GuestBooking { GuestId = 5, EventId = 6 },  // Charlie at Bob's Surprise Birthday

             // Guest 6 (David Martinez)
             new GuestBooking { GuestId = 6, EventId = 5 },  // David at Digital Marketing Summit

             // Guest 7 (Eve Miller)
             new GuestBooking { GuestId = 7, EventId = 6 },  // Eve at Bob's Surprise Birthday
             new GuestBooking { GuestId = 7, EventId = 9 },  // Eve at Frank's 50th Birthday Party

             // Guest 8 (Frank Wilson)
             new GuestBooking { GuestId = 8, EventId = 7 },  // Frank at Sophie and Adam's Wedding

             // Guest 9 (Grace Taylor)
             new GuestBooking { GuestId = 9, EventId = 7 },  // Grace at Sophie and Adam's Wedding
             new GuestBooking { GuestId = 9, EventId = 8 },  // Grace at AI and Future Technologies

             // Guest 10 (Hank Anderson)
             new GuestBooking { GuestId = 10, EventId = 10 } // Hank at Business Leadership Forum
            );

            //Seed data to populate Staff Table
            builder.Entity<Staff>().HasData(
            new Staff { StaffId = 1, FirstName = "Sarah", LastName = "Jones", IsFirstAider = true },
            new Staff { StaffId = 2, FirstName = "Jacob", LastName = "Smith", IsFirstAider = false },
            new Staff { StaffId = 3, FirstName = "Emily", LastName = "Brown", IsFirstAider = true },
            new Staff { StaffId = 4, FirstName = "Michael", LastName = "Johnson", IsFirstAider = false },
            new Staff { StaffId = 5, FirstName = "Jessica", LastName = "Williams", IsFirstAider = true },
            new Staff { StaffId = 6, FirstName = "David", LastName = "Miller", IsFirstAider = false },
            new Staff { StaffId = 7, FirstName = "Laura", LastName = "Wilson", IsFirstAider = false },
            new Staff { StaffId = 8, FirstName = "Daniel", LastName = "Taylor", IsFirstAider = false },
            new Staff { StaffId = 9, FirstName = "Sophia", LastName = "Davis" , IsFirstAider = true },
            new Staff { StaffId = 10, FirstName = "James", LastName = "Anderson" , IsFirstAider = false }
            );

            //Seed data to populate Staffing Table
            builder.Entity<Staffing>().HasData(
            new Staffing { StaffId = 1, EventId = 1 },  // Sarah at John and Jane's Wedding
            new Staffing { StaffId = 2, EventId = 1 },  // Jacob at John and Jane's Wedding
            new Staffing { StaffId = 3, EventId = 2 },  // Emily at Tech Innovations 2022
            new Staffing { StaffId = 4, EventId = 2 },  // Michael at Tech Innovations 2022
            new Staffing { StaffId = 5, EventId = 3 },  // Jessica at Alice's Birthday Party
            new Staffing { StaffId = 6, EventId = 3 },  // David at Alice's Birthday Party
            new Staffing { StaffId = 7, EventId = 4 },  // Laura at Mike and Emma's Wedding
            new Staffing { StaffId = 8, EventId = 4 },  // Daniel at Mike and Emma's Wedding
            new Staffing { StaffId = 9, EventId = 5 },  // Sophia at Digital Marketing Summit
            new Staffing { StaffId = 10, EventId = 5 }, // James at Digital Marketing Summit
            new Staffing { StaffId = 1, EventId = 6 },  // Sarah at Bob's Surprise Birthday Party
            new Staffing { StaffId = 2, EventId = 6 },  // Jacob at Bob's Surprise Birthday Party
            new Staffing { StaffId = 3, EventId = 7 },  // Emily at Sophie and Adam's Wedding
            new Staffing { StaffId = 4, EventId = 7 },  // Michael at Sophie and Adam's Wedding
            new Staffing { StaffId = 5, EventId = 8 },  // Jessica at AI and Future Technologies
            new Staffing { StaffId = 6, EventId = 8 },  // David at AI and Future Technologies
            new Staffing { StaffId = 7, EventId = 9 },  // Laura at Frank's 50th Birthday Party
            new Staffing { StaffId = 8, EventId = 9 },  // Daniel at Frank's 50th Birthday Party
            new Staffing { StaffId = 9, EventId = 10 }, // Sophia at Business Leadership Forum
            new Staffing { StaffId = 10, EventId = 10 } // James at Business Leadership Forum
            );
        }
    }
}
