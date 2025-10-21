## `bakery-api` — ASP.NET Core Web API

### Description
A RESTful API that manages **customers**, **products**, and **orders** for the *Belle Croissant Lyonnais* bakery system.  
It provides endpoints for authentication, product management, and order handling, serving as the backend for the `bakery-app` mobile application.

### Features
- Built with **ASP.NET Core Web API**
- Supports CRUD operations for:
  - Customers
  - Products
  - Orders 
- Integrated with SQL Server
- Proper data validation and exception handling
- Optimized for mobile API consumption

---

### Setup Instructions

#### 1. Clone the repository
```bash
git clone https://github.com/Shaikhaalzaabi2004/bakery-api.git
cd bakery-api
```

#### 2. Configure the database connection
In `appsettings.json` or inside `BakeryContext.cs`, **update the connection string** to match your local environment:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_LOCAL_IP\\SQLEXPRESS;Initial Catalog=Session6;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
}
```

> **Note:**  
> Replace `YOUR_LOCAL_IP` with your **local machine IP address** (e.g., `192.168.1.198`).  
> This IP must match the one used in the **mobile app’s `ApiHelper`** class.

#### 3. Apply migrations and seed data
If using EF Core:
```bash
dotnet ef database update
```

Alternatively, execute the SQL script found in the mobile app repository (`bakery-app/SqlScript/script.sql`).

#### 4. Run the API
```bash
dotnet run
```

Your API should now be running on:
```
http://<your-ip>:5168
```

---


### Related Repositories
[**bakery-app** (MAUI Mobile App)](https://github.com/Shaikhaalzaabi2004/bakery-app)

---

### License
This project is part of the **Belle Croissant Lyonnais** solution — developed for educational and competitive purposes under the **IT Software Solutions for Business** framework.

---
