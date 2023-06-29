### FAQ.API - Project Overview
  * Welcome to the FAQ Web API repository! This project is (Frequently Asked Questions) system built using .NET 7 and SQL Server. It   
    provides a RESTful API interface for managing and retrieving frequently asked questions and their corresponding answers.

### Key Features:
  * Flexible API Endpoints: The API offers a range of endpoints to create, update, delete.
  * Structured Data Storage: The FAQ data is stored in a SQL Server database, ensuring scalability, reliability, and easy maintenance.
  * CORS (Cross-Origin Resource Sharing): The API includes CORS support, allowing client applications to access the API from different domains.
  * Dependency Injection: The project utilizes the built-in dependency injection feature of .NET, facilitating the management and injection of dependencies.
  * AutoMapper: The API integrates AutoMapper, simplifying the mapping between different data models within the application.
  * API Rate Limiting: The API incorporates rate-limiting functionality to control the number of requests a client can make within a specified time period.
  * Custom API Authorization: Custom API authorization mechanisms are implemented to secure the endpoints and restrict access to authorized users only. This 
    ensures that only authenticated and authorized clients can interact with the API.
  * Secure Authentication: You can implement secure authentication mechanisms, such as JWT (JSON Web Tokens), to control access to the API endpoints and protect 
    sensitive data.

### To run this project : 
 1. Make sure you have SQL Server installed.
 2. Open the appsettings.json which is located in the FAQ.API project and change the stmp and API settings to your settings.
 ```
   "EmailSettings": {
    "From": "your email",
    "SmtpServer": "your smtp",
    "Port": 587,
    "SmtpUsername": "your smtp username",
    "SmtpPassword": "your smtp password",
    "FromEmail": "the from email",
    "UseSSL": true,
    "IsBodyHtml": true,
    "Host": "your host",
    "Subject": "Email cofirmation",
    "Body": "Thank you for choosing FAQ-Q. Use the following OTP to complete your Sign Up procedures. OTP: {OTP} is ......"
  }
```
```
"API_KEY": "a api 🗝",
```
 3. Build and Run the project.
