# Web Bank Application built with C# ASP.NET Core MVC and Razor Views

## Project Description
This project implements a simple bank system with essential functionalities. It is a web application built using ASP.NET Core MVC, with Razor Views rendering server-side HTML pages. The backend exposes HTTP endpoints that handle business logic and serve these views. While the application provides RESTful-style endpoints, it is not a pure REST API returning JSON, but rather a server-rendered web app with HTTP endpoints delivering HTML content. This project was created as part of a university coursework.

## Key Functionalities of the Project
- User registration and login
- Ability to create, view, and manage bank accounts
- Transactions between accounts (withdrawal, transfer)
- Viewing transaction history
- Currency conversion during transfers, using real-time exchange rates from an external API
- Cookie-based authentication

## Database Schema (PostgreSQL hosted on Railway)
The following is the database schema created using PostgreSQL and hosted on the Railway cloud platform, defining the structure for users, accounts, and transactions.

<p align="center">
  <img src="https://github.com/user-attachments/assets/bcbbe16c-d6ef-4c69-985f-270b110a7f3c">
</p>

## Endpoints
Below are the API documentation, created using Swagger, details the available endpoints. Please note that the Razor views handle only GET and POST requests, which is a common limitation due to HTML form method support.

<p align="center">
  <img src="https://github.com/user-attachments/assets/9b45b2ed-2d63-4d8b-b1af-e82e5eb72fa1">
</p>

### Authorization and Authentication of Local Users
User authentication is managed using ASP.NET Core Cookie Authentication, which provides secure login and session management through cookies. This approach enables server-side session tracking and enforces authorization policies based on user roles, ensuring that only authenticated users can access protected resources.

## App Screenshot

<p align="center">
  <img src="https://github.com/user-attachments/assets/f5150261-32ee-47a7-af91-ea7822a8d77a" width="200"/>
  <img src="https://github.com/user-attachments/assets/19d0ddc8-4304-4a56-84a3-50e88c4b1902" width="200"/>
  <img src="https://github.com/user-attachments/assets/2c5b5fe6-b4b4-42f9-a573-f2112d05b8a4" width="200"/>
</p>

## Technologies Used 
- C#
- ASP.NET Core
- Razor Views
- Entity Framework Core for ORM and database schema migrations
- PostgreSQL hosted on Railway
- AutoMapper
- BCrypt.Net for passwords hashing
- Swagger for API documentation
- ASP.NET Core Cookie Authentication

## Useful Applications 
- Postman
- JetBrains Rider
- GitHub
