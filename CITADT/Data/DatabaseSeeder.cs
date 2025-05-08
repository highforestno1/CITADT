using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CITADT.Models;

namespace CITADT.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Seed Roles
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Seed Admin User
            var adminEmail = "admin@citadt.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed Categories
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category
                    {
                        Name = "Tin tức",
                        Description = "Tin tức chung",
                        Slug = "tin-tuc",
                        DisplayOrder = 1,
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    },
                    new Category
                    {
                        Name = "Thông báo",
                        Description = "Thông báo chung",
                        Slug = "thong-bao",
                        DisplayOrder = 2,
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    },
                    new Category
                    {
                        Name = "Sự kiện",
                        Description = "Các sự kiện sắp diễn ra",
                        Slug = "su-kien",
                        DisplayOrder = 3,
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    }
                };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // Seed News
            if (!context.News.Any())
            {
                var news = new List<News>
                {
                    new News
                    {
                        Title = "Chào mừng đến với CITADT",
                        Content = "Nội dung bài viết chào mừng...",
                        Summary = "Bài viết chào mừng người dùng đến với hệ thống",
                        Slug = "chao-mung-den-voi-citadt",
                        IsPublished = true,
                        PublishedAt = DateTime.Now,
                        AuthorId = adminUser.Id,
                        CategoryId = context.Categories.First().Id,
                        CreatedAt = DateTime.Now
                    }
                };
                await context.News.AddRangeAsync(news);
                await context.SaveChangesAsync();
            }

            // Seed Notifications
            if (!context.Notifications.Any())
            {
                var notifications = new List<Notification>
                {
                    new Notification
                    {
                        Title = "Hệ thống đã được cập nhật",
                        Message = "Hệ thống đã được cập nhật lên phiên bản mới",
                        Type = NotificationType.General,
                        IsActive = true,
                        CreatedAt = DateTime.Now
                    },
                    new Notification
                    {
                        Title = "Bảo trì hệ thống",
                        Message = "Hệ thống sẽ được bảo trì vào ngày mai",
                        Type = NotificationType.Warning,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        ExpiryDate = DateTime.Now.AddDays(1)
                    }
                };
                await context.Notifications.AddRangeAsync(notifications);
                await context.SaveChangesAsync();
            }
        }
    }
} 