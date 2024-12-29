# Hosting Process Equipment API

This API manages the hosting of process equipment for production facilities. It provides functionality to manage contracts, ensure proper allocation of equipment, and handle background tasks using Azure Queue. The API is secured with API Key authentication.

---

## Table of Contents
1. [Features](#features)
2. [Requirements](#requirements)
3. [Getting Started](#getting-started)
4. [API Documentation](#api-documentation)


---

## Features
- **Contract Management**: Create, read, and manage hosting contracts for production facilities.
- **Background Processing**: Asynchronous processing of equipment placement contracts using Azure Queue.
- **API Key Authentication**: Secures API endpoints with a static API Key.
- **CORS Support**: Ensures cross-origin requests are properly handled.
- **Swagger Integration**: API documentation and testing through Swagger UI.

---

## Requirements
- **.NET 8.0**
- **Azure SQL Database**
- **Azure App Service**
- **Azure Queue Storage** (Optional for background processing)

---

## Getting Started

### Prerequisites
- Install [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Set up an **Azure SQL Database**.
- Optionally, configure **Azure Queue Storage**.

### Clone the Repository
```bash
git clone https://github.com/nikita1grynenko/hosting-process-equipment-service.git
cd hosting-process-equipment-service
```

### API Documentation
The API documentation is available via Swagger UI:  

- Local: https://localhost:5001/swagger/index.html  
Example Endpoints
```bash
GET /api/Contracts/GetContracts: Retrieve all contracts.
POST /api/Contracts/CreateContract: Create a new contract.
```
