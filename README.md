# GamehubAPI

## Overview

GamehubAPIAPI is a RESTful web API built with ASP.NET Core.It's a fictional online gaming store specializing in computer games, It provides endpoints for managing resource for game.It supports basic CRUD operations for our game catalog also allows listing and pagination of game entries. This project serves as a foundation for building scalable and maintainable web applications.

## Features

- RESTful API architecture
- Built with ASP.NET Core
- Support for JSON data format
- Swagger documentation for API endpoints

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022 or later](https://visualstudio.microsoft.com/downloads/) (optional)
- [Postman](https://www.postman.com/) or another API client for testing

## Getting Started

### Clone the repository

```bash
git clone https://github.com/Jishnuram-Chathoth/GamehubAPI.git
cd GamehubAPI

Install dependencies
Navigate to the project directory and run the following command:

dotnet restore

Run the application
You can run the application using the following command:

dotnet run

## API Documentation

API documentation is available at https://localhost:7085/swagger once the application is running. Use this to explore the available endpoints and test them interactively.


### API Endpoints
  
- `GET/api/Game/All`  -Retrieves  list of games.
   Implemented Pagination
   Accepts two querystring parameters PageNumber and Pagesize(Default value is 1 and 10 respectively)
   
   Response:

   200 OK: Returns a list of games.

- `GET /api/Game/{id}` - Retrieves a specific game by ID.
   
   Response:

   200 OK: Returns the resource.
   BadRequest - 400 - Badrequest 
   404 Not Found: If the resource does not exist.


- `GET /api/Game/GetGameByTitle` - Retrieves a specific game by TItle.
   
   Response:

   200 OK: Returns the resource.
   BadRequest - 400 - Badrequest 
   404 Not Found: If the resource does not exist.

- `GET /api/Game/GetGameByGenre` - Retrieves a specific game by Genre.


    Response:

   200 OK: Returns the resource.
   BadRequest - 400 - Badrequest 
   404 Not Found: If the resource does not exist.


- `POST /api/Game/Create` - Creates a new game

Request Body:


{

  "title": "string",
  "genre": "string",
  "description": "string",
  "price": 0,
  "releaseDate": "2024-10-22T19:29:39.656Z",
  "stockQty": 0
}

Response:

201 Created: Returns the created resource.
400 Bad Request: If the request is invalid.

.
- `PUT /api/Game/Update` - Updates an existing value.


Updates an existing resource.

Request Body:

{
  "id": 0,
  "title": "string",
  "genre": "string",
  "description": "string",
  "price": 0,
  "releaseDate": "2024-10-22T19:32:33.739Z",
  "stockQty": 0
}
Response:

200 OK: Returns the updated resource.
404 Not Found: If the resource does not exist.



- `DELETE /api/Game/Delete/{id}` - Deletes a resource by ID.

Response:

204 No Content: If the deletion is successful.
404 Not Found: If the resource does not exist.




