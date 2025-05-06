# CITADT - Database Setup

## Database Tables
The application uses the following tables:
- Users: Stores user information
- Categories: Stores news categories
- Tags: Stores tags for news articles
- News: Stores news articles
- NewsTags: Junction table for the many-to-many relationship between News and Tags

## Entity Framework Migrations

To set up the database, follow these steps:

1. Open a terminal in the project directory
2. Run the following commands to create and apply migrations:

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

If you need to make changes to the database schema, create a new migration:

```
dotnet ef migrations add [MigrationName]
dotnet ef database update
```

## Connection String

The default connection string in `appsettings.json` is configured to use SQL Server. Update it if you need to connect to a different database server.

## Seed Data

The application is configured with seed data that will be automatically applied when the database is created. This includes:

### Users
- Administrator (admin@citadt.dhhp.edu.vn)
- Editor (editor@citadt.dhhp.edu.vn)
- Giảng Viên (giangvien@citadt.dhhp.edu.vn)

### Categories
- Tin tức
- Đào tạo
- Nghiên cứu khoa học
- Chuyển đổi số
- Sự kiện

### Tags
- CNTT
- Chuyển đổi số
- Đào tạo
- Chứng chỉ
- Công nghệ
- IoT
- AI
- Big Data

### News
Sample news articles with proper relationships to categories, tags, and authors.

You can either:
1. Let the application create and seed the database on startup (default behavior)
2. Use Entity Framework migrations as described above