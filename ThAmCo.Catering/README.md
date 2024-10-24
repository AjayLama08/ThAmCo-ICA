
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
### Clone the Repository
git clone https://github.com/AjayLama08/ThAmCo-ICA/tree/master/ThAmCo.Catering


### Setup the Database
1. Open the integrated terminal in Visual Studio (Tools > NuGet Package Manager > Package Manager Console).
2. Run the following command to add and apply migrations:
>> Add-Migration InitialCreate -Context CateringContext	Data/Migrations
3. Run the following command to update the database:
>> Update-Database


### Running the Application
1. Open the solution in Visual Studio.
2. Set `ThAmCo.Catering` as the startup project (right-click the project in Solution Explorer > Set as StartUp Project).)
3. Press `F5` to run the application.

## API Endpoints
- `GET /api/FoodBookings` - Retrieve all food bookings
- `GET /api/FoodBookings/{id}` - Retrieve a specific food booking by ID
- `POST /api/FoodBookings` - Create a new food booking
- `PUT /api/FoodBookings/{id}` - Update an existing food booking
- `DELETE /api/FoodBookings/{id}` - Delete a food booking

## Project Structure
- `Controllers/` - Contains API controllers
- `Data/` - Contains Entity Framework Core DbContext and entity classes
- `Models/` - Contains data models

## Contributing
1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -am 'Add new feature'`).
4. Push to the branch (`git push origin feature-branch`).
5. Create a new Pull Request.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact
For any questions or suggestions, please contact [q2043246@live.tees.ac.uk](mailto: q2043246@live.tees.ac.uk).
