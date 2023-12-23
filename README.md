# AlpataTech.MeetingAppDemo
This repository contains a Dockerized setup for a meeting organization application which I created during my summer internship at [Alpata Yazılım ve Teknoloji](https://www.linkedin.com/company/alpatateknoloji) which consists MSSQL Server database, an Angular frontend, and a .NET Core API.

## Prerequisites

- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)

## Getting Started

1. Clone this repository:

    ```bash
    git clone https://github.com/seppacar/AlpataTech-MeetingAppDemo.git
    cd meeting-app-demo
    ```

2. Build and run the application using Docker Compose:

    ```bash
    docker-compose up -d
    ```

    This will start the SQL Server, API, and Angular frontend in detached mode.

3. Wait for the services to fully initialize. You can check the logs for each service:

    ```bash
    docker-compose logs -f
    ```

    Wait until you see messages indicating that the services are ready.

4. Access the application:

    - Frontend: Open your browser and go to [http://localhost:4200](http://localhost:4200)
    - API Swagger UI: [http://localhost:5000/swagger](http://localhost:5000/swagger)
    - API Index HTML: [http://localhost:5000](http://localhost:5000)

5. Stop the application when you're done:

    ```bash
    docker-compose down
    ```

## Environment Variables

- **SQL Server:**
  - `SA_PASSWORD`: Your Super Secret Password for the SQL Server.

- **API:**
  - `ASPNETCORE_ENVIRONMENT`: Set to `Development` for development environment.
  - `DB_CONNECTION_STRING_PROD`: Connection string for production database.
  - `DB_CONNECTION_STRING_DEV`: Connection string for development database.

## Notes

- Make sure ports `4200`, `5000`, and `1433` are not in use on your machine.

- The database will be available on `localhost:1433`, the API on `localhost:5000`, and the frontend on `localhost:4200`.

- The API is configured to wait for the SQL Server to be ready before starting.
