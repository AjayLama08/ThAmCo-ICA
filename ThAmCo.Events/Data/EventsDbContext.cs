using Microsoft.EntityFrameworkCore;
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
            
                
        }
    }
}
