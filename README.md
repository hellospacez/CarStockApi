# CarStockApi



### API Test URL: [https://carstockapi.onrender.com/](https://carstockapi.onrender.com/swagger)

### Svelte Frontend Test URL: [https://car-api-front.vercel.app/](https://car-api-front.vercel.app/)

### Svelte Frontend GitHub: [https://github.com/hellospacez/CarApiFront](https://github.com/hellospacez/CarApiFront)




Make sure you have the following installed **before running or building the project**:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) ✅ (required to build)
- [.NET 8.0 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0/runtime) ✅ (required to run)


  
This project has been tested on:
- Windows 11 + IIS Express + .NET 8 ✅
- Ubuntu 22.04 + Nginx + .NET 8 ✅

- Unit tests are written using [xUnit]



## Setup Instructions

1. Clone the repo
2. Run the following commands:
    - Make sure you have .NET 8.0.
    - Run `dotnet build` to build the project.
    - Run `dotnet run` to start the application.
3. SQLite file `carstock.db` will be created automatically if missing.

## Endpoints

### Car Endpoints

This API follows standard **RESTful** principles

- `POST /car` – Add a new car to the dealer’s inventory
Request body: { "make": "Audi", "model": "A4", "year": 2018, "stock": 10 }
Responses: 200 OK, 400 Bad Request, 401 Unauthorized

#### Validation Rules

- **make**: Required, 1–50 characters.
- **model**: Required, 1–50 characters.
- **year**: Must be between 1900 and 2100.
- **stock**: Must be between 0 and 9999.

If the input fails validation, the API returns:

```json
{
  "errors": {
    "Make": ["Make is required."],
    "Model": ["Model cannot be longer than 50 characters."],
    "Year": ["Year must be between 1900 and 2100."],
    "Stock": ["Stock must be a positive number."]
  }
}
```

- `DELETE /car/{id}` – Delete a specific car by ID from the dealer’s inventory
Path parameter: id (int)
Responses: 200 OK, 404 Not Found, 401 Unauthorized

- `GET /car` – List all cars owned by the dealer, or filter by make and model
Query parameters (optional): make, model
Responses: 200 OK, 401 Unauthorized

- `PUT /car/{id}` – Update the stock level of a specific car
Path parameter: id (int)
Request body: { "stock": 8 }
Responses: 200 OK, 404 Not Found, 401 Unauthorized

### Auth Endpoints

- `POST /register` – Register a new dealer
- `POST /login` – Login to get a JWT token

### Auth Details

- Use JWT tokens for authentication:
    - **Generate token** using `POST /login`
    - **Pass token** in the header as `Authorization: Bearer <token>`

## Tools Used

- FastEndpoints
- Dapper
- SQLite
- JWT Authentication

## Architecture

### Key Design Decisions:

- **SOLID Principles**: Followed SOLID principles to maintain clean and maintainable architecture.
- **FastEndpoints**: Used FastEndpoints to create clean and easy-to-manage REST API endpoints.
- **Dapper**: Utilized Dapper for fast and efficient database operations.
- **JWT Authentication**: Token-based authentication for secure access to the endpoints.

## Installation

1. Clone the repository
2. Run the project using `dotnet run`
3. Access the endpoints via `http://localhost:5100`



## Example Usage:

### Login:

Send a `POST` request to `/login` with the following body:

```json
{
  "username": "dealer1",
  "password": "password123"
}
```


## Swagger:
![swagger-screenshot.png](/CarStockApi/swagger-screenshot.png)
