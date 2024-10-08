### Prerequisites

Before you can run this project, you need the following:
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

### Step 1: Run SQL Server in Docker

Run the following command to set up a **SQL Server** instance using Docker. This will start the SQL Server container on port `1433` with a password for the `SA` user.

```bash
docker run \
  -e 'ACCEPT_EULA=Y' \
  -e 'SA_PASSWORD=Ahlpay123.' \
  -p 1433:1433 \
  --name sql_server \
  -d mcr.microsoft.com/mssql/server:2022-latest 
  ```

### Step 2: Connect to SQL Server with SSMS
Once the SQL Server is running in Docker, you can connect to it using SQL Server Management Studio (SSMS) to view or modify the database.

- Open SSMS.
- In the Server name field, enter `localhost,1433` .
- For Authentication, select SQL Server Authentication.
- In the Login field, enter `SA`.
- In the Password field, enter `Ahlpay123.` .
- Trust server certificate
- Click Connect.
Now, you should be connected to the SQL Server instance running in Docker and can manage your database from there.

### Step 3: Setting up database

- Set `API` as startup project 
- Open Package Manager Console
- Set `Infrastructure` as default project
- Type update-database and enter
Now, go to SMSS and refresh the server, you should be able to see database with tables. 
You can now start the project and work on it.
Password for users are -> `ahlpw`