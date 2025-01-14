# ThAmCo.Catering

## Overview
ThAmCo.Catering is a .NET 8 web application designed to manage catering services, including food bookings, menus, and food items. This project uses Entity Framework Core for database operations and ASP.NET Core for building the web API.

## Features
- Manage food bookings
- Manage menus and food items
- RESTful API endpoints for CRUD operations

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

## Getting Started
1. Clone the Repository.
   - Open visual studio.
   - Click on the "Clone a repository" button.
   - Enter the repository URL (which is https://github.com/AjayLama08/ThAmCo-ICA/tree/master/ThAmCo.Catering in this case).
   - Click on the "Clone" button.

### Setup the Database
1. Open the integrated terminal in Visual Studio (Tools > NuGet Package Manager > Package Manager Console).
2. Run the following command to update the database:
   - `Update-Database`

### Running the Application
1. Open the solution in Visual Studio.
2. Set `ThAmCo.Catering` as the startup project (right-click the project in Solution Explorer > Set as StartUp Project).)
3. Press `F5` to run the application.

## API Endpoints

1. **Food Items**
- `GET /api/FoodItems` - Retrieve all food items
- `GET /api/FoodItems/{id}` - Retrieve a specific food item by ID
- `POST /api/FoodItems` - Create a new food item
- `PUT /api/FoodItems/{id}` - Edit an existing food item
- `DELETE /api/FoodItems/{id}` - Delete a food item

2. **Menus**
- `GET /api/Menus` - Retrieve all menus
- `GET /api/Menus/{id}` - Retrieve a specific menu by ID
- `POST /api/Menus` - Create a new menu
- `PUT /api/Menus/{id}` - Edit an existing menu
- `DELETE /api/Menus/{id}` - Delete a menu

3. **Menu Food Items**
- `GET /api/MenuFoodItems` - Retrieve all menu food items
- `GET /api/MenuFoodItems/{id}` - Retrieve a specific menu food item by ID
- `POST /api/MenuFoodItems` - Create a new menu food item
- `PUT /api/MenuFoodItems/{id}` - Edit an existing menu food item
- `DELETE /api/MenuFoodItems/{id}` - Delete a menu food item

4. **Food Bookings**
- `GET /api/FoodBookings` - Retrieve all food bookings
- `GET /api/FoodBookings/{id}` - Retrieve a specific food booking by ID
- `POST /api/FoodBookings` - Create a new food booking
- `PUT /api/FoodBookings/{id}` - Edit an existing food booking
- `DELETE /api/FoodBookings/{id}` - Delete a food booking

## Key Problems and Challenges  

1. **Database Migrations**: Keeping the database schema updated with new features like food items, orders, and event assignments was crucial and complex.  
2. **API Design**: Building a RESTful API for managing catering services required a clear structure to handle food orders efficiently.  

## Alternative Solutions  

1. **Database Migrations** 					 
    - **Alternative**: Raw SQL scripts for schema updates.  
    - **Why Not Used**: More prone to errors and time-consuming.  

2. **API Design**  
   - **Alternative**: GraphQL for more flexible queries.  
   - **Why Not Used**: RESTful API fits the project's scale and simplicity needs.  

## Justification  

- **Entity Framework Core**: Simplifies database operations and supports seamless integration with ASP.NET Core.  
- **ASP.NET Core**: A reliable framework for building APIs with tools like routing, dependency injection, and middleware.  
- **RESTful API**: A widely accepted standard for managing resources like food orders, ensuring ease of use and compatibility.  

## Contact
For any questions or suggestions, please contact [q2043246@live.tees.ac.uk](mailto:q2043246@live.tees.ac.uk).

