# RecipeSharing


## Prerequisites

Before running this application, ensure that you have the following prerequisites installed on your machine:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 7.0 )
- [Git](https://git-scm.com/downloads)

## Getting Started (CLI)

Follow the steps below to set up and run the application locally using the command-line interface (CLI):

1. **Clone the repository**

   ```shell
   git clone https://github.com/idejaferati/RecipeSharing.git
 
2. **Navigate to the project directory**

   ```shell
   cd your-repo directorium


3. **Restore dependencies**

Run the following command to restore all the required dependencies for the project:

   
    dotnet restore


4. **Create the database**

Execute the following command to apply migrations and create the database:

    
    dotnet ef database update


5. **Run the application**

Start the web API application using the following command:

    
    dotnet run

6. **Test the API**

Use tools like Postman, Advanced REST Client, or any other HTTP client to test the API endpoints exposed by the application.

## Getting Started (Visual Studio 2022)

Follow the steps below to set up and run the application locally using Visual Studio 2022:

1. **Clone the repository**

    ```shell
    git clone https://github.com/idejaferati/RecipeSharing.git

2. **Open the project in Visual Studio**

Open Visual Studio 2022 and select "Open a project or solution."
Navigate to the cloned repository folder and select the project solution file (.sln) to open the project.


3. **Restore dependencies**

In Visual Studio, go to Tools -> NuGet Package Manager -> Package Manager Console.
Execute the following command in the Package Manager Console:

    
    dotnet restore

4.**Create the database**

From the Package Manager Console, execute the following command to apply migrations and create the database:

    
    Update-Database

5. **Run the application**

Press F5 or click on the "Start Debugging" button in Visual Studio to build and run the application. Visual Studio will automatically launch the web API and open it in your default browser.

6. **Test the API**

Use Swagger or any other API testing tool to test the API endpoints exposed by the application.
