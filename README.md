# Sensors

## Setup

### Backend - .NET Web API

1. **Clone the Repository**
    ```sh
    git clone https://romanpalenha04@dev.azure.com/romanpalenha04/sensors-dashboard/_git/sensors-dashboard
    cd sensors-dashboard
    ```

2. **Navigate to the API Project Directory**
    ```sh
    cd Sensors
    ```

3. **Restore Dependencies**
    ```sh
    dotnet restore
    ```

4. **Update Database (if applicable)**
    ```sh
    cd ..\Sensors.Domain\
    dotnet ef database update
    cd ..\Sensors\
    ```

5. **Run the API**
    ```sh
    dotnet run
    ```

    The API should now be running at `http://localhost:5224` (or another configured port).

### Frontend - Angular Application

1. **Navigate to the Angular Project Directory**
    ```sh
    cd sensors-dashboard
    ```

2. **Install Dependencies**
    ```sh
    npm install
    ```

3. **Run the Angular Application**
    ```sh
    ng serve
    ```

    The Angular application should now be running at `http://localhost:4200`.

## Running the Applications

1. **Start the .NET API** (if not already running)
    ```sh
    cd Sensors
    dotnet run
    ```

2. **Start the Angular Application** (if not already running)
    ```sh
    cd sensors-dashboard
    ng serve
    ```

3. **Access the Application**
    Open your web browser and go to `http://localhost:4200`. The Angular frontend should be able to communicate with the .NET backend.

## Additional Configuration

- **Environment Variables**: Ensure your environment variables are set up correctly for both development and production environments.
- **Configuration Files**: Check `appsettings.json` (for .NET) and `environment.ts` (for Angular) for any additional configuration settings required.

## Azure Deployment

### Deploying .NET Web API to Azure

1. **Create an Azure App Service**
    - Navigate to the [Azure Portal](https://portal.azure.com/).
    - Create a new App Service and configure it for your .NET application.

2. **Deploy the API**
    - Use the following commands to publish your .NET application to Azure:
    ```sh
    dotnet publish -c Release
    az webapp deploy --resource-group <ResourceGroupName> --name <AppName> --src-path <PublishDirectory>
    ```

### Deploying Angular Application to Azure

1. **Create an Azure Static Web App**
    - Navigate to the [Azure Portal](https://portal.azure.com/).
    - Create a new Static Web App and link it to your Angular project repository.

2. **Deploy the Angular Application**
    - Run deploy pipeline in Pipelines

