# 📇 Contacts Manager (ASP.NET Core MVC)

This repository contains a **.NET 8.0 MVC application** for managing contacts with advanced functionalities and modern software engineering practices.  
The solution demonstrates concepts such as **Clean Architecture**, **Repository & Unit of Work patterns**, **Authentication & Authorization with Identity & Roles**, **Logging**, **Testing**, and exporting data as **PDF/CSV/Excel**.  

---

## **What’s Implemented in This Project**

### 1. **Clean Architecture & SOLID Principles**
- Structured into layers (**Core**, **Infrastructure**, **UI**, **Tests**).  
- Enforced separation of concerns and testability.  

### 2. **Entity Framework Core Integration**
- **SQL Server**.  
- Repository & Unit of Work patterns for data access.  
- LINQ for querying and data manipulation.  

### 3. **Authentication & Authorization**
- **ASP.NET Core Identity** for user management.  
- Role-based and policy-based authorization.  

### 4. **Logging**
- **Serilog** for structured logging and better monitoring.  

### 5. **Testing**
- **xUnit, Moq, Fluent Assertions** for unit testing.  
- **Integration Testing** for end-to-end scenarios.  

### 6. **Export Features**
- Generate and download **PDF, CSV, Excel** reports.  

### 7. **Asynchronous Programming**
- Used `async` and `await` for scalability and responsiveness.  

### 8. **MVC UI**
- Fully responsive UI with **Bootstrap**.  
- CRUD operations for managing persons and countries.  
- Search, filter, and newsletter subscription features.  

---

## **Technologies Used**
- **.NET 8.0 MVC**  
- **Entity Framework Core**  
- **Repository & Unit of Work Patterns**  
- **Serilog Logging**  
- **AutoMapper**  
- **ASP.NET Core Identity**  
- **xUnit, Moq, Fluent Assertions**  
- **Integration Testing**  
- **LINQ**  
- **Bootstrap**  
- **SQL Server**  

---

## **Project Structure**

```
ContactsManager
│
├── ContactsManager.Core             # Core layer (business logic & contracts)
│   ├── DTO                          # Data Transfer Objects
│   ├── Domain                       # Domain entities (Person, Country, etc.)
│   ├── Enums                        # Enumerations
│   ├── Exceptions                   # Custom exception handling
│   ├── Helpers                      # Helper classes
│   ├── ServiceContracts             # Interfaces for services
│   └── Services                     # Business services implementations
│
├── ContactsManager.Infrastructure   # Data access & persistence
│   ├── DbContext                    # ApplicationDbContext and EF Core configs
│   ├── Migrations                   # EF Core migrations
│   └── Repositories                 # Repository & Unit of Work implementations
│
├── ContactsManager.UI               # ASP.NET Core MVC (Presentation Layer)
│   ├── Controllers                  # MVC Controllers (Person, Country, Account)
│   ├── Filters                      # Custom filters
│   ├── Middleware                   # Custom middleware
│   ├── Properties                   # Project properties
│   ├── StartupExtensions            # Extension methods for DI & middleware
│   ├── Views                        # Razor views (CRUD, Authentication, etc.)
│   └── wwwroot                      # Static files (Bootstrap, CSS, JS, images)
│
├── ContactsManager.ControllerTests  # Unit tests for Controllers
│
├── ContactsManager.ServiceTests     # Unit tests for Services
│
└── ContactsManager.IntegrationTests # Integration tests (end-to-end)
```

---

## **How To Run**

### 1. Clone the Repository
```bash
git clone https://github.com/your-username/ContactsManager.git
cd ContactsManager
```

### 2. Update Database Connection
Modify **appsettings.json** inside `ContactsManager.UI` with your SQL Server connection string.  

### 3. Apply Database Migrations
```bash
dotnet ef database update --project ContactsManager.Infrastructure
```

### 4. Run the Application
```bash
dotnet run --project ContactsManager.UI
```

### 5. Access the App
Open your browser at:  
👉 `https://localhost:5001`
