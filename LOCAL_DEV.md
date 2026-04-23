# Bookistore Local Development Guide

## Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [SQL Server LocalDB](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb) or SQL Server instance

## Running Locally

1. **Set connection string** in `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "BOOKIESTORE_DB": "Server=(localdb)\\mssqllocaldb;Database=BookstoreDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

2. **Run migrations** (if database doesn't exist):
```bash
dotnet ef database update
```

3. **Start the app**:
```bash
dotnet run
```

The app will be available at `http://localhost:5000`

## For Production (Fly.io with SQLite)
No additional configuration needed. The app automatically uses SQLite (`bookstore.db`) when running in production mode.

## Key Differences: Local vs Production
| Feature | Local (Development) | Production (Fly.io) |
|---------|---------------------|---------------------|
| Database | SQL Server | SQLite |
| Connection String | appsettings.Development.json | BOOKIESTORE_DB env var |
| Port | 5000/5001 | 8080 |