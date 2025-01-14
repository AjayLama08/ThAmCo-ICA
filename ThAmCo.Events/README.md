# ThAmCo.Events

## Overview
ThAmCo.Events is a .NET 8 project designed to manage events for ThAmCo. This project includes functionality for creating, updating, and managing events, as well as handling bookings and customer information.

## Features
- Create and manage events
- Handle customer bookings
- Manage event schedules
- Integration with other ThAmCo services

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

## Getting Started
1. Clone the Repository.
   - Open visual studio.
   - Click on the "Clone a repository" button.
   - Enter the repository URL (which is https://github.com/AjayLama08/ThAmCo-ICA/tree/master/ThAmCo.Events in this case).
   - Click on the "Clone" button.

### Setup the Database
1. Open the integrated terminal in Visual Studio (Tools > NuGet Package Manager > Package Manager Console).
2. Run the following command to update the Events database:
   - `Update-Database -context EventsDbContext`
3. Run the following command to update the Identity database:
   - `Update-Database -context IdentityDbContext`

### Running the Application
1. Open the solution in Visual Studio.
2. Set `ThAmCo.Events` and `ThAmcCo.Venues` as the startup project (right-click the solution  in Solution Explorer > Configure Startup Projects > Multiple Startups Projects > Set the action to start for both projects).
3. Press `F5` to run the application.

## Key Problems and Challenges  

1. **Database Migrations**: Keeping the database schema updated with event details, venue bookings, and staff assignments was crucial and challenging.  
2. **API Design**: Creating a RESTful API for managing event-related operations required careful structuring to ensure ease of use.  
3. **Concurrency**: Managing simultaneous updates to events, bookings, and assignments without conflicts was a significant challenge.  

## Alternative Solutions  

1. **Database Migrations**  
   - **Alternative**: Use raw SQL scripts for schema updates.  
   - **Why Not Used**: Manual effort and higher error risk compared to Entity Framework migrations.  

2. **API Design**  
   - **Alternative**: Use GraphQL for flexible data querying.  
   - **Why Not Used**: RESTful API was more suitable for the scale and simplicity of this project.  

3. **Concurrency**  
   - **Alternative**: Implement a distributed lock manager like Redis.  
   - **Why Not Used**: Application-level locking and database constraints were sufficient for the project.  

## Justification  

- **Entity Framework Core**: Chosen for its ability to streamline database operations and simplify schema updates with migrations.  
- **ASP.NET Core**: A robust framework providing tools like dependency injection, middleware, and routing to build efficient APIs.  
- **RESTful API**: A widely adopted standard, making it easier for clients to interact with event, venue, and staff resources seamlessly.  

## Contact
For any questions or suggestions, please contact [q2043246@live.tees.ac.uk](mailto:q2043246@live.tees.ac.uk).
