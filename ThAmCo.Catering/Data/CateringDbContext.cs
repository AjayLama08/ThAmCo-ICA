using Microsoft.EntityFrameworkCore;

namespace ThAmCo.Catering.Data
{
    public class CateringDbContext : DbContext
    {
        //DbSets which represent the tables in the database
        public DbSet<FoodBooking> FoodBookings { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuFoodItem> MenuFoodItems { get; set; }

        //Path to the database file
        public string DbPath { get; set; } = string.Empty;

        //Constructor to set the path to the database file
        public CateringDbContext()
        {
            var folder = Environment.SpecialFolder.MyDocuments;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "ThAmCo.Catering.db");
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

            //Compoound key
            builder.Entity<MenuFoodItem>().HasKey(mfi => new { mfi.MenuId, mfi.FoodItemId });

            //Foreign key constraints
            builder.Entity<MenuFoodItem>()
                .HasOne(fi => fi.FoodItem) 
                .WithMany(mfi => mfi.MenuFoodItems) //One to many relationship
                .HasForeignKey(fi => fi.FoodItemId) //Foreign key
                .OnDelete(DeleteBehavior.Cascade); //Restrict deletion of FoodItem if it is referenced in MenuFoodItem

            builder.Entity<MenuFoodItem>()
                .HasOne(m => m.Menu)
                .WithMany(mfi => mfi.MenuFoodItems)
                .HasForeignKey(m => m.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            //Seed data to Menu to populate the database
            builder.Entity<Menu>().HasData(
                new Menu { MenuId = 1, MenuName = "Vegetarian" },
                new Menu { MenuId = 2, MenuName = "Non-Vegetarian" },
                new Menu { MenuId = 3, MenuName = "Vegan" },
                new Menu { MenuId = 4, MenuName = "Gluten-Free" },
                new Menu { MenuId = 5, MenuName = "Pescatarian" },
                new Menu { MenuId = 6, MenuName = "Keto" },
                new Menu { MenuId = 7, MenuName = "Paleo" },
                new Menu { MenuId = 8, MenuName = "Low-Carb" },
                new Menu { MenuId = 9, MenuName = "High-Protein" },
                new Menu { MenuId = 10, MenuName = "Dairy-Free" },
                new Menu { MenuId = 11, MenuName = "Nut-Free" },
                new Menu { MenuId = 12, MenuName = "Halal" },
                new Menu { MenuId = 13, MenuName = "Kosher" },
                new Menu { MenuId = 14, MenuName = "Organic" },
                new Menu { MenuId = 15, MenuName = "Mediterranean" }
);
            //Seed data to FoodBooking to populate the database
            builder.Entity<FoodBooking>().HasData(
                new FoodBooking { FoodBookingId = 1, ClientReferenceId = 101, NumberOfGuests = 10, MenuId = 1 },
                new FoodBooking { FoodBookingId = 2, ClientReferenceId = 102, NumberOfGuests = 20, MenuId = 2 },
                new FoodBooking { FoodBookingId = 3, ClientReferenceId = 103, NumberOfGuests = 30, MenuId = 3 },
                new FoodBooking { FoodBookingId = 4, ClientReferenceId = 104, NumberOfGuests = 15, MenuId = 4 },
                new FoodBooking { FoodBookingId = 5, ClientReferenceId = 105, NumberOfGuests = 25, MenuId = 5 },
                new FoodBooking { FoodBookingId = 6, ClientReferenceId = 106, NumberOfGuests = 35, MenuId = 6 },
                new FoodBooking { FoodBookingId = 7, ClientReferenceId = 107, NumberOfGuests = 40, MenuId = 7 },
                new FoodBooking { FoodBookingId = 8, ClientReferenceId = 108, NumberOfGuests = 12, MenuId = 8 },
                new FoodBooking { FoodBookingId = 9, ClientReferenceId = 109, NumberOfGuests = 18, MenuId = 9 },
                new FoodBooking { FoodBookingId = 10, ClientReferenceId = 110, NumberOfGuests = 22, MenuId = 10 },
                new FoodBooking { FoodBookingId = 11, ClientReferenceId = 111, NumberOfGuests = 28, MenuId = 11 },
                new FoodBooking { FoodBookingId = 12, ClientReferenceId = 112, NumberOfGuests = 32, MenuId = 12 },
                new FoodBooking { FoodBookingId = 13, ClientReferenceId = 113, NumberOfGuests = 45, MenuId = 13 },
                new FoodBooking { FoodBookingId = 14, ClientReferenceId = 114, NumberOfGuests = 50, MenuId = 14 },
                new FoodBooking { FoodBookingId = 15, ClientReferenceId = 115, NumberOfGuests = 60, MenuId = 15 }
);
            //Seed data to FoodItem to populate the database
            builder.Entity<FoodItem>().HasData(
                new FoodItem { FoodItemId = 1, Description = "Spicy Chicken Curry with Coconut Milk", UnitPrice = 10.50f },
                new FoodItem { FoodItemId = 2, Description = "Steamed Jasmine Rice", UnitPrice = 5.00f },
                new FoodItem { FoodItemId = 3, Description = "Hard-Boiled Free-Range Eggs", UnitPrice = 3.50f },
                new FoodItem { FoodItemId = 4, Description = "Grilled Salmon with Lemon Butter Sauce", UnitPrice = 18.75f },
                new FoodItem { FoodItemId = 5, Description = "Caesar Salad with Grilled Chicken", UnitPrice = 12.00f },
                new FoodItem { FoodItemId = 6, Description = "Margherita Pizza with Fresh Basil", UnitPrice = 15.00f },
                new FoodItem { FoodItemId = 7, Description = "Beef Burger with Caramelized Onions", UnitPrice = 14.25f },
                new FoodItem { FoodItemId = 8, Description = "Vegan Quinoa Salad with Avocado", UnitPrice = 11.50f },
                new FoodItem { FoodItemId = 9, Description = "Creamy Mushroom Risotto", UnitPrice = 16.30f },
                new FoodItem { FoodItemId = 10, Description = "BBQ Ribs with Coleslaw", UnitPrice = 20.00f },
                new FoodItem { FoodItemId = 11, Description = "Chocolate Lava Cake with Vanilla Ice Cream", UnitPrice = 8.75f },
                new FoodItem { FoodItemId = 12, Description = "Garlic Butter Shrimp Skewers", UnitPrice = 19.90f },
                new FoodItem { FoodItemId = 13, Description = "Vegetable Stir-Fry with Tofu", UnitPrice = 9.40f },
                new FoodItem { FoodItemId = 14, Description = "Penne Alfredo with Grilled Chicken", UnitPrice = 13.60f },
                new FoodItem { FoodItemId = 15, Description = "Fresh Fruit Platter with Seasonal Berries", UnitPrice = 7.20f }
);
            //Seed data to MenuFoodItem to populate the database
            builder.Entity<MenuFoodItem>().HasData(
                new MenuFoodItem { MenuId = 1, FoodItemId = 1 }, // Vegetarian Menu with Spicy Chicken Curry
                new MenuFoodItem { MenuId = 1, FoodItemId = 2 }, // Vegetarian Menu with Steamed Jasmine Rice
                new MenuFoodItem { MenuId = 2, FoodItemId = 3 }, // Non-Vegetarian Menu with Hard-Boiled Free-Range Eggs
                new MenuFoodItem { MenuId = 2, FoodItemId = 4 }, // Non-Vegetarian Menu with Grilled Salmon
                new MenuFoodItem { MenuId = 3, FoodItemId = 5 }, // Vegan Menu with Caesar Salad
                new MenuFoodItem { MenuId = 3, FoodItemId = 8 }, // Vegan Menu with Quinoa Salad
                new MenuFoodItem { MenuId = 4, FoodItemId = 7 }, // Gluten-Free Menu with Beef Burger
                new MenuFoodItem { MenuId = 5, FoodItemId = 12 }, // Pescatarian Menu with Garlic Butter Shrimp
                new MenuFoodItem { MenuId = 6, FoodItemId = 9 }, // Keto Menu with Mushroom Risotto
                new MenuFoodItem { MenuId = 7, FoodItemId = 13 }, // Paleo Menu with Vegetable Stir-Fry
                new MenuFoodItem { MenuId = 8, FoodItemId = 11 }, // Low-Carb Menu with Chocolate Lava Cake
                new MenuFoodItem { MenuId = 9, FoodItemId = 14 }, // High-Protein Menu with Penne Alfredo
                new MenuFoodItem { MenuId = 10, FoodItemId = 6 }, // Dairy-Free Menu with Margherita Pizza
                new MenuFoodItem { MenuId = 11, FoodItemId = 10 }, // Nut-Free Menu with BBQ Ribs
                new MenuFoodItem { MenuId = 12, FoodItemId = 15 }  // Halal Menu with Fresh Fruit Platter
);




        }
    }
}

