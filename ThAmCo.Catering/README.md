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
- `GET /api/FoodBookings` - Retrieve all food bookings
- `GET /api/FoodBookings/{id}` - Retrieve a specific food booking by ID
- `POST /api/FoodBookings` - Create a new food booking
- `PUT /api/FoodBookings/{id}` - Edit an existing food booking
- `DELETE /api/FoodBookings/{id}` - Delete a food booking

## Key Problems and Challenges  

1. **Database Migrations**: Keeping the database schema updated with new features like food items, orders, and event assignments was crucial and complex.  
2. **API Design**: Building a RESTful API for managing catering services required a clear structure to handle food orders efficiently.  
3. **Concurrency**: Managing simultaneous food orders and updates without conflicts was a significant challenge.  

## Alternative Solutions  

1. **Database Migrations** 					 
    - **Alternative**: Raw SQL scripts for schema updates.  
    - **Why Not Used**: More prone to errors and time-consuming.  

2. **API Design**  
   - **Alternative**: GraphQL for more flexible queries.  
   - **Why Not Used**: RESTful API fits the project's scale and simplicity needs.  

3. **Concurrency**  
   - **Alternative**: Redis for distributed locking.  
   - **Why Not Used**: Application-level locking and SQL constraints were sufficient.  

## Justification  

- **Entity Framework Core**: Simplifies database operations and supports seamless integration with ASP.NET Core.  
- **ASP.NET Core**: A reliable framework for building APIs with tools like routing, dependency injection, and middleware.  
- **RESTful API**: A widely accepted standard for managing resources like food orders, ensuring ease of use and compatibility.  

## Contact
For any questions or suggestions, please contact [q2043246@live.tees.ac.uk](mailto: q2043246@live.tees.ac.uk).
