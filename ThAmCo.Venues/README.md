# ThAmCo.Venues

## Overview
ThAmCo.Venues is a .NET 8 project designed to manage venue bookings for various events. This project provides functionalities to create, update, and manage venues, as well as handle bookings and availability.

## Features
- Venue Management: Add, update, and delete venues.
- Booking Management: Create and manage bookings for different events.
- Availability Checking: Check the availability of venues for specific dates.
- Integration with ThAmCo.Events and ThAmCo.Catering for seamless event planning.

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

## Getting Started
1. Clone the Repository.
   - Open visual studio.
   - Click on the "Clone a repository" button.
   - Enter the repository URL (which is https://github.com/AjayLama08/ThAmCo-ICA/tree/master/ThAmCo.Venues in this case).
	- Click on the "Clone" button.

## Setup the Database
1. Open the integrated terminal in Visual Studio (Tools > NuGet Package Manager > Package Manager Console).
2. Run the following command to update the Venue database:
   - `Update-Database -context VenueDbContext`

### Running the Application
To run the application, use the following command:
1. Open the solution in Visual Studio.
2. Set `ThAmCo.Venues` as the startup project (right-click the project in Solution Explorer > Set as StartUp Project).
3. Press `F5` to run the application.

## Contact
For any inquiries or support, please contact [yourname@yourdomain.com](mailto:yourname@yourdomain.com).
