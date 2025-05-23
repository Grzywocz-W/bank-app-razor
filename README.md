# REST API of the Bank Application Project

## Project Description
The project enables the operation of a bank system by providing necessary functionalities. The system consists of a web application built with ASP.NET Core, using Razor views for the frontend. The backend is powered by REST API, which handles the core logic of bank operations.

## Key Functionalities of the Project
- User registration and login
- Authentication via JWT
- Ability to create, view, and manage bank accounts
- Transactions between accounts (deposit, withdrawal, transfer)
- Generating financial reports (statements, transaction history)

## Use Case Diagram
This diagram shows the use cases necessary for the project to function. The backend also implements additional functionalities not shown in the diagram below.

<p align="center">
  <img width="75%" height="75%" src="https://github.com/YourUsername/BankApp/blob/main/readmeImages/UseCases.png">
</p>

## Database Schema (SQL Server)
The following is the database schema created using SQL Server, which defines the structure for users, accounts, and transactions.

<p align="center">
  <img src="https://github.com/user-attachments/assets/bcbbe16c-d6ef-4c69-985f-270b110a7f3c">
</p>

## Endpoints
Below are the key endpoints of the REST API, shown with sample responses from the Postman application.

<p align="center">
  <img width="75%" height="75%" src="https://github.com/YourUsername/BankApp/blob/main/readmeImages/Postman.png">
</p>

Sample payment API documentation:
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

## Technologies Used to Create the Backend
- C#
- ASP.NET Core
- Razor Views
- Entity Framework Core
- JWT Authentication
- SQL Server
- Google OAuth
- AutoMapper
- Swagger for API documentation

## Useful Applications for Creating the Backend
- Postman
- Visual Studio
- GitHub
- Swagger UI
- Trello
