# Tailoring API

## Starting SQL server
```powerShell
$sa_password="[SA PASSWORD HERE]"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -p 1433:1433 -v sqlvolume:/var/opt/mssql -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04

```

## Setting the connection string to secret manager
```powerShell
$sa_password="[SA PASSWORD HERE]"

dotnet user-secrets set "ConnectionStrings:TailoringContext" "server=localhost; Database=Tailoring; User Id=sa; Password=$sa_password;Trusted_Connection=True; TrustServerCertificate=True;"

```