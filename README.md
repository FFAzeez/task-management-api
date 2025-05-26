Team Task Management API

Overview

A RESTful API for managing team-based tasks with user authentication, team collaboration, and task tracking, built with .NET 8, EF Core, and SQL Server using clean architecture principles.

Tech Stack
Framework: .NET 8
Database: SQL Server with EF Core (Code-First)
Authentication: JWT
Logging:inbuilt log
Documentation: Swagger
Testing: xUnit
Architecture: Clean Architecture (Domain, Business, Infrastructure, API)

Setup Steps

1. Clone the Repository
git clone https://github.com/your-repo/task-management-api.git
2. Configure SQL Server
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TaskManagementDb;Trusted_Connection=True;"
}
3. Access Swagger
http://localhost:5077/swagger/index.html

API Usage Examples
1. POST  https://localhost:5001/api/auth/register
2. POST https://localhost:5001/api/auth/login 
3. GET https://localhost:5001/api/auth/me
4. POST https://localhost:5001/api/teams
5. POST https://localhost:5001/api/teams/{teamId}/users
6. POST https://localhost:5001/api/teams/{teamId}/tasks 
7. GET https://localhost:5001/api/teams/{teamId}/tasks
8. PUT https://localhost:5001/api/teams/{teamId}/tasks/{taskId} 
9. DELETE https://localhost:5001/api/teams/{teamId}/tasks/{taskId}
10.PATCH https://localhost:5001/api/teams/{teamId}/tasks/{taskId}/status

Assumptions
Passwords are hashed using SHA256.
Team admins can add users and delete tasks; members can create, update, and change task status.
Task access is scoped to team membership.
SQL Server is used as the database; connection string must be configured.
JWT tokens are valid for 1 hour.
Role-based permissions are implemented with "Admin" and "Member" roles.
Minimal input validation is implemented; additional validation can be added as needed.

Project Structure
TeamTaskManagementAPI.Domain: Entities, interfaces, and domain logic
TeamTaskManagementAPI.Business: DTOs, services, and business logic
TeamTaskManagementAPI.Infrastructure: EF Core, repositories, and data access
TeamTaskManagementAPI: Controllers, middleware, and API configuration
TeamTaskManagementAPI.Test: Unit tests with xUnit

