# Web Bank Application built with C# ASP.NET Core MVC and Razor Views

## Project Description
This project implements a bank system with essential functionalities. It is a web application built using ASP.NET Core MVC, with Razor Views rendering server-side HTML pages. The backend exposes HTTP endpoints that handle business logic and serve these views. While the application provides RESTful-style endpoints, it is not a pure REST API returning JSON, but rather a server-rendered web app with HTTP endpoints delivering HTML content.

## Key Functionalities of the Project
- User registration and login
- Ability to create, view, and manage bank accounts
- Transactions between accounts (withdrawal, transfer)
- Viewing transaction history
- Currency conversion during transfers, using real-time exchange rates from an external API

## Use Case Diagram
This diagram shows the use cases necessary for the project to function. The backend also implements additional functionalities not shown in the diagram below.

<p align="center">
  <img width="75%" height="75%" src="https://github.com/YourUsername/BankApp/blob/main/readmeImages/UseCases.png">
</p>

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

## Description of the Implementation of the Most Important Functionalities - Backend

### Authorization and Authentication of Local Users
User authentication is managed via JWT (JSON Web Tokens), which are issued during the login process. JWTs allow secure authentication for the user and enable protected API access.

<p align="center">
  <img width="75%" height="75%" src="https://github.com/YourUsername/BankApp/blob/main/readmeImages/JWT.png">
</p>

### Google Authentication
Google authentication is implemented using ASP.NET Core Identity and Google OAuth. Below is the flow of the user login process via Google.

<p align="center">
  <img width="75%" height="75%" src="https://github.com/YourUsername/BankApp/blob/main/readmeImages/GoogleAuthenticationFlow.png">
</p>

### Account Management
The backend allows users to manage their accounts, including viewing balances, making deposits and withdrawals, and transferring funds between accounts. These functionalities are accessible via secure RESTful endpoints.

### Transactions and Transfers
The system supports various types of transactions, including:
- Deposits to an account
- Withdrawals from an account
- Transfers between accounts

Each transaction is recorded in the database for auditing and reporting purposes.

### Generating Financial Reports
Reports are generated using the application’s built-in reporting feature. Two main types of reports are available:
1. **Client Report**: A detailed financial statement of the user's account activity.
2. **Admin Report**: A summary of all transactions in the system for admin users.

Example of a client report:

<p align="center">
  <img width="50%" height="50%" src="https://github.com/YourUsername/BankApp/blob/main/readmeImages/ClientReport.png">
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

## Useful Applications 
- Postman
- JetBrains Rider
- GitHub
