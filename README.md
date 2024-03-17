# User Authentication System

# Description
This project is a full-stack user authentication system, featuring a front end built with Angular 16 and utilizing reactive forms for login and registration. The backend is developed with .NET 6.0, and authentication is managed through JWT (JSON Web Tokens) authorization. This setup provides a secure and scalable solution for user management.

# Features
- User Registration
- User Login
- JWT Authorization
- Profile Management (Basic)

# Requirements
Node.js (for Angular CLI)
.NET SDK 6.0
SQL Server (or any relational database for user data storage)

# Installation
- Frontend Setup
Ensure Node.js is installed.
Install Angular CLI globally: npm install -g @angular/cli
Navigate to the frontend directory: cd path/to/frontend
Install dependencies: npm install
Serve the application: ng serve
Visit http://localhost:4200/ in your browser.

- Backend Setup
Ensure .NET SDK 6.0 is installed.
Navigate to the backend directory: cd path/to/backend
Restore the project dependencies: dotnet restore
Start the project: dotnet run
The backend will start on http://localhost:5000/ by default.
Usage

# Registration
Navigate to http://localhost:4200/register in your web browser.
Fill in the required fields (e.g., username, password, email) in the registration form.
Submit the form. The user will be registered and redirected to the login page.

# Login
Navigate to http://localhost:4200/login.
Enter your username and password.
Submit the form. Upon successful authentication, the JWT token will be stored locally, and the user will be redirected to their profile page.

# Contributing
Contributions are welcome! Please fork the repository and submit pull requests with any enhancements, bug fixes, or improvements.
