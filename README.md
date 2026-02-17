# Clean Architecture API - Production Ready

A production-ready ASP.NET Core Web API built with Clean Architecture principles, featuring comprehensive error handling, transaction management, and best practices.

## 🏗️ Architecture

```
Api → Application → Domain
      ↑
Infrastructure
```

### Layers:

- **Domain Layer**: Core business entities and interfaces (no dependencies)
- **Application Layer**: Business logic, DTOs, validators, and service interfaces
- **Infrastructure Layer**: Data access, repositories, external services
- **API Layer**: Controllers, middleware, and API configuration
- **Common Layer**: Shared models, exceptions, and utilities

## ✨ Features

✅ Clean Architecture (Onion Architecture)  
✅ Generic API Response Pattern  
✅ Global Exception Handling Middleware  
✅ Unit of Work Pattern (Transaction Management)  
✅ Repository Pattern  
✅ FluentValidation for Input Validation  
✅ Result Pattern for Operation Outcomes  
✅ Serilog for Advanced Logging  
✅ API Versioning  
✅ CORS Configuration  
✅ Response Caching  
✅ Rate Limiting  
✅ Health Checks  
✅ Swagger/OpenAPI Documentation  
✅ Request/Response Logging  
✅ Custom Domain Exceptions  

## 🚀 Getting Started

### Prerequisites

- .NET 7.0 SDK or later
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/satyaambati5/my-clean-architecture-api.git
cd my-clean-architecture-api
```

2. **Update connection string**

Edit `MyProject.Api/appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MyProjectDb;Trusted_Connection=true"
}
```

3. **Run migrations**
```bash
cd MyProject.Api
dotnet ef database update --project ../MyProject.Infrastructure
```

4. **Run the application**
```bash
dotnet run
```

The API will be available at `https://localhost:7001` (or check console output)

## 📁 Project Structure

```
MyProject/
├── MyProject.Common/              # Shared utilities
│   ├── Models/
│   │   ├── ApiResponse.cs        # Generic API response
│   │   └── Result.cs             # Result pattern
│   └── Exceptions/
│       └── CustomExceptions.cs   # Domain exceptions
│
├── MyProject.Domain/              # Core domain
│   ├── Entities/
│   │   └── Product.cs
│   └── Interfaces/
│       ├── IProductRepository.cs
│       └── IUnitOfWork.cs
│
├── MyProject.Application/         # Business logic
│   ├── DTOs/
│   │   └── ProductDto.cs
│   ├── Interfaces/
│   │   └── IProductService.cs
│   ├── Services/
│   │   └── ProductService.cs
│   └── Validators/
│       └── ProductDtoValidator.cs
│
├── MyProject.Infrastructure/      # Data access
│   ├── Data/
│   │   ├── ApplicationDbContext.cs
│   │   └── UnitOfWork.cs
│   └── Repositories/
│       └── ProductRepository.cs
│
└── MyProject.Api/                 # API layer
    ├── Controllers/
    │   └── ProductsController.cs
    ├── Middleware/
    │   ├── ExceptionHandlingMiddleware.cs
    │   └── RequestResponseLoggingMiddleware.cs
    ├── Filters/
    │   └── ValidationFilter.cs
    └── Program.cs
```

## 🔌 API Endpoints

### Products

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/v1/products` | Get all products |
| GET | `/api/v1/products/{id}` | Get product by ID |
| POST | `/api/v1/products` | Create new product |
| PUT | `/api/v1/products/{id}` | Update product |
| DELETE | `/api/v1/products/{id}` | Delete product |

### Health Check

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/health` | API health status |

## 📝 API Response Format

### Success Response
```json
{
  "success": true,
  "message": "Product created successfully",
  "data": {
    "id": 1,
    "name": "Laptop",
    "price": 999.99
  },
  "errors": [],
  "statusCode": 200,
  "timestamp": "2026-02-17T10:30:00Z"
}
```

### Error Response
```json
{
  "success": false,
  "message": "Validation failed",
  "data": null,
  "errors": [
    "Product name is required",
    "Price must be greater than 0"
  ],
  "statusCode": 422,
  "timestamp": "2026-02-17T10:30:00Z"
}
```

## 🔒 Exception Handling

The API includes comprehensive exception handling:

- **NotFoundException** (404): Resource not found
- **ValidationException** (422): Validation errors
- **BadRequestException** (400): Invalid request
- **UnauthorizedException** (401): Authentication required
- **ForbiddenException** (403): Insufficient permissions
- **ConflictException** (409): Resource conflict
- **BusinessException** (400): Business rule violations

## 🔄 Transaction Management

Uses Unit of Work pattern for database transactions:

```csharp
await _unitOfWork.BeginTransactionAsync();
try
{
    // Multiple operations
    await _unitOfWork.Products.AddAsync(product);
    await _unitOfWork.Inventory.AddAsync(inventory);
    
    // All succeed together
    await _unitOfWork.CommitTransactionAsync();
}
catch
{
    // All fail together
    await _unitOfWork.RollbackTransactionAsync();
    throw;
}
```

## 📊 Logging

Logs are written to:
- Console (all environments)
- File: `logs/log-YYYYMMDD.txt` (rolling daily)

## 🧪 Testing

```bash
# Run tests (when added)
dotnet test
```

## 🌿 Branch Strategy

- **main**: Production-ready code
- **development**: Active development

## 🛠️ Technologies

- ASP.NET Core 7.0+
- Entity Framework Core
- FluentValidation
- Serilog
- Swagger/OpenAPI
- SQL Server

## 📚 Learn More

- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)

## 👤 Author

**satyaambati5**

## 📄 License

This project is licensed under the MIT License.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

⭐ If you find this helpful, please star the repository!