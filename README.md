# 🧩 .NET 10 Person & Store Microservices Sample
> [!NOTE]
> This repository is an educational code sample showcasing the coding style,
> project structure organization, and software architecture approach of
> **Amirhossein Tohidi**. It has been created as a technical portfolio project
> to demonstrate clean layered architecture practices, service‑oriented and
> microservice development patterns using **.NET 10**.
>
> This solution was implemented as part of a **time‑boxed coding challenge**.
> Therefore, additional projects and class libraries were intentionally avoided
> to keep the structure as simple as possible. The main focus is on delivering
> an acceptable solution with **simplicity, readability, and fast delivery**,
> rather than on extensive modularization.


A lightweight sample demonstrating two independent **.NET 10 Minimal API services** built with a **clean layered architecture**, **EF Core**, and **SQL Server**.

This project demonstrates a simple **microservice-style architecture** where one service communicates with another to retrieve customer information when creating invoices.

🔗 Repository:
https://github.com/amirhossein-tohidi/person-store-microservices-sample

---

# 🏗 Architecture Overview

The repository contains two independent services:

- 👤 **PersonService** – manages people data
- 🧾 **StoreService** – manages invoices and communicates with PersonService

Each service has its **own solution, database, and dependencies** to simulate real microservice boundaries.

```
person-store-microservices-sample
 ├─ services
 │   ├─ PersonService
 │   └─ StoreService
 └─ README.md
```

---

#⚙️ Technologies

- ✅ .NET 10
- ✅ Minimal APIs
- ✅ Entity Framework Core
- ✅ SQL Server
- ✅ HttpClientFactory
- ✅ OpenAPI
- ✅ Scalar API UI

Architecture concepts used:

- Clean Architecture (lightweight)
- SOLID principles
- Repository Pattern
- Dependency Injection
- Idempotency
- Snapshot Pattern

---

# 👤 PersonService

PersonService is responsible for managing **person information**.

Example responsibilities:

- retrieving a person by national code
- validating national codes
- providing person information to other services

Layer structure:

```
PersonService/
└── PersonService.Api/
    ├── Properties/
    │   └── launchSettings.json
    │
    ├── API/
    │   ├── DTOs/
    │   │   └── PersonResponseDto.cs
    │   │
    │   ├── Endpoints/
    │   │   └── PersonEndpoints.cs
    │   │
    │   ├── Extensions/
    │   │   ├── ApplicationBuilderExtensions.cs
    │   │   └── ServiceCollectionExtensions.cs
    │   │
    │   └── Validators/
    │       └── NationalCodeValidator.cs
    │
    ├── Application/
    │   ├── Interfaces/
    │   │   └── IPersonRepository.cs
    │   │
    │   └── Services/
    │       └── PersonAppService.cs
    │
    ├── Domain/
    │   └── Person.cs
    │
    ├── Infrastructure/
    │   ├── Persistence/
    │   │   ├── Configurations/
    │   │   │   └── PersonConfiguration.cs
    │   │   │
    │   │   ├── Migrations/
    │   │   │   ├── *timestamp*_InitialCreate.cs
    │   │   │   └── PersonDbContextModelSnapshot.cs
    │   │   │
    │   │   ├── Repositories/
    │   │   │   └── PersonRepository.cs
    │   │   │
    │   │   ├── PersonDbContext.cs
    │   │   └── PersonDbSeeder.cs
    │
    ├── appsettings.json
    ├── appsettings.Development.json
    └── Program.cs


```

Key points:

- Domain entities encapsulate behavior
- EF Core configurations use `IEntityTypeConfiguration`
- Database migrations run automatically on startup
- Database seeding is **idempotent**

---

# 🧾 StoreService

StoreService is responsible for **invoice creation**.

When creating an invoice, the service retrieves customer information from **PersonService**.

Responsibilities:

- create invoices
- prevent duplicate invoice creation
- store a snapshot of customer information

Layer structure:

```
StoreService/
└── StoreService.Api/
    ├── API/
    │   ├── DTOs/
    │   │   ├── CreateInvoiceItemDto.cs
    │   │   ├── CreateInvoiceRequestDto.cs
    │   │   ├── CreateProductRequestDto.cs
    │   │   ├── InvoiceItemResponseDto.cs
    │   │   ├── InvoiceResponseDto.cs
    │   │   ├── PersonDto.cs
    │   │   └── ProductResponseDto.cs
    │   │
    │   ├── Endpoints/
    │   │   ├── InvoiceEndpoints.cs
    │   │   └── ProductEndpoints.cs
    │   │
    │   └── Extensions/
    │       ├── ApplicationBuilderExtensions.cs
    │       └── ServiceCollectionExtensions.cs
    │
    ├── Application/
    │   ├── Interfaces/
    │   │   ├── ExternalServices/
    │   │   │   └── IPersonServiceClient.cs
    │   │   │
    │   │   └── Repositories/
    │   │       ├── Common/
    │   │       │   ├── IBaseRepository.cs
    │   │       │   └── IUnitOfWork.cs
    │   │       ├── IInvoiceRepository.cs
    │   │       └── IProductRepository.cs
    │   │
    │   └── Services/
    │       ├── InvoiceAppService.cs
    │       └── ProductAppService.cs
    │
    ├── Domain/
    │   ├── Common/
    │   │   ├── BaseEntity.cs
    │   │   └── IEntity.cs
    │   │
    │   ├── Entities/
    │   │   ├── Invoice.cs
    │   │   ├── InvoiceItem.cs
    │   │   └── Product.cs
    │   │
    │   └── ValueObjects/
    │       ├── CreationToken.cs
    │       └── CustomerInfo.cs
    │
    ├── Infrastructure/
    │   ├── ExternalServices/
    │   │   └── PersonServiceClient.cs
    │   │
    │   ├── Persistence/
    │   │   ├── Configurations/
    │   │   │   ├── InvoiceConfiguration.cs
    │   │   │   ├── InvoiceItemConfiguration.cs
    │   │   │   └── ProductConfiguration.cs
    │   │   │
    │   │   ├── Migrations/
    │   │   │   ├── *timestamp*_InitialCreate.cs
    │   │   │   └── StoreDbContextModelSnapshot.cs
    │   │   │
    │   │   ├── Repositories/
    │   │   │   ├── Common/
    │   │   │   │   ├── BaseRepository.cs
    │   │   │   │   └── UnitOfWork.cs
    │   │   │   ├── ProductRepository.cs
    │   │   │   └── InvoiceRepository.cs
    │   │   │
    │   │   ├── StoreDbContext.cs
    │   │   └── StoreDbSeeder.cs
    │   │   
    │
    ├── appsettings.json
    ├── appsettings.Development.json
    └── Program.cs

```

External communication is implemented via:

- **PersonServiceClient** using `HttpClientFactory`

---

# 🧠 Key Design Concepts

## 🔁 Idempotency

Invoice creation supports an **IdempotencyKey**.

If the same request is sent multiple times with the same key:

- only **one invoice** will be created
- the previously created invoice will be returned

This prevents duplicate operations in distributed systems.

---

## 📸 Snapshot Pattern

Invoices store a **snapshot of customer data** at creation time.

Example stored fields:

```
CustomerFirstName
CustomerLastName
CustomerNationalCode
```

This ensures invoice history remains unchanged even if the customer's data is modified later.

---

## 🗄 Repository Pattern

Repositories abstract EF Core from the application logic.

Benefits:

- better separation of concerns
- easier testing
- reduced coupling with EF Core

Example:

```
IPersonRepository
PersonRepository
```

---

# 🚀 Running the Services

Clone the repository:

```
git clone https://github.com/amirhossein-tohidi/person-store-microservices-sample.git
```

Navigate to the services directory:

```
cd services
```

Run **PersonService**:

```
cd PersonService
 dotnet run
```

Service URL:

```
https://localhost:5001
```

Run **StoreService** in another terminal:

```
cd StoreService
 dotnet run
```

Service URL:

```
https://localhost:5002
```

---

# 📄 API Documentation

Interactive API documentation is available using **Scalar**.

PersonService:

```
https://localhost:5001/scalar
```

StoreService:

```
https://localhost:5002/scalar
```

---

# 🎯 Purpose of This Project

This repository demonstrates how to build **small but production‑style services with .NET Minimal APIs** while keeping the architecture clean and maintainable.

It focuses on:

- clear service boundaries
- layered architecture
- service‑to‑service communication
- idempotent operations
- maintainable code structure

---

⭐ If you find this project useful, feel free to star the repository.
