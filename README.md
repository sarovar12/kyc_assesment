## API Documentation for KYC Assesment

### Entity Controller

#### `GET /api/entity`

- **Description:** Retrieves all entities.
- **Parameters:**
  - `search` (optional): A string to filter entities by name.
- **Returns:** An array of entity objects.
- **HTTP Status Codes:**
  - 200 OK: Request successful.
  - 400 Bad Request: Unable to process the request.

#### `GET /api/entity/{id}`

- **Description:** Retrieves an entity by ID.
- **Parameters:**
  - `id`: The ID of the entity.
- **Returns:** The entity object.
- **HTTP Status Codes:**
  - 200 OK: Request successful.
  - 400 Bad Request: Unable to process the request.

#### `POST /api/entity`

- **Description:** Creates a new entity.
- **Request Body:** EntityRequestDTO object.
- **Returns:** Success message if creation is successful.
- **HTTP Status Codes:**
  - 200 OK: Entity created successfully.
  - 400 Bad Request: Unable to process the request.

#### `PUT /api/entity`

- **Description:** Updates an existing entity.
- **Request Body:** EntityUpdateDTO object.
- **Returns:** Success message if update is successful.
- **HTTP Status Codes:**
  - 200 OK: Entity updated successfully.
  - 400 Bad Request: Unable to process the request.

#### `DELETE /api/entity/{id}`

- **Description:** Deletes an entity by ID.
- **Parameters:**
  - `id`: The ID of the entity to delete.
- **Returns:** Success message if deletion is successful.
- **HTTP Status Codes:**
  - 200 OK: Entity deleted successfully.
  - 400 Bad Request: Unable to process the request.

#### `GET /api/entity/filtered`

- **Description:** Retrieves entities with advanced filtering options.
- **Parameters:**
  - `search` (optional): A string to filter entities by name.
  - `gender` (optional): Gender filter.
  - `startDate` (optional): Start date filter.
  - `endDate` (optional): End date filter.
  - `countries` (optional): List of countries to filter entities.
- **Returns:** An array of entity objects.
- **HTTP Status Codes:**
  - 200 OK: Request successful.
  - 400 Bad Request: Unable to process the request.

## API Use Guide

To use the Entity Controller API, follow the instructions below:

1. **Update Migration:** Ensure that your database schema is up-to-date with the latest changes. If you are using a Code First approach, follow these steps:
   - Open the Package Manager Console in Visual Studio.
   - Run the command `Add-Migration <MigrationName>` to create a new migration with a descriptive name.
   - Run the command `Update-Database` to apply the pending migrations and update the database schema.

2. **Check Connection String:** Verify that your application's database connection string is correctly configured in the `appsettings.json` or equivalent configuration file.

3. **API Endpoints:**
   - Utilize the provided endpoints to interact with the Entity data.
   - You can retrieve all entities, get an entity by ID, add a new entity, update an existing entity, or delete an entity.

4. **Request Format:**
   - For POST and PUT requests, provide the required data in the request body in the specified format (EntityRequestDTO for creation, EntityUpdateDTO for updates).

5. **Response Handling:**
   - Handle responses with appropriate HTTP status codes and error messages.
   - Successful requests will return the requested data or a success message.

6. **Advanced Filtering:**
   - Use the `/api/entity/filtered` endpoint for advanced filtering based on search criteria, gender, date range, and countries.

7. **Error Handling:**
   - Handle errors gracefully by checking the HTTP status codes and parsing error messages from responses.
   - If a request fails, review the provided error message for troubleshooting.

8. **Security:**
   - Ensure that appropriate authentication and authorization mechanisms are in place to secure access to the API endpoints.

By following these steps, you can effectively utilize the Entity Controller API in your application.
h criteria, gender, date range, and countries.

