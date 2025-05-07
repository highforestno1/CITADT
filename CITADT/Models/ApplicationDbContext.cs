using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CITADT.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsTag> NewsTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<News>()
                .HasOne(n => n.Category)
                .WithMany(c => c.News)
                .HasForeignKey(n => n.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<News>()
                .HasOne(n => n.Author)
                .WithMany(u => u.News)
                .HasForeignKey(n => n.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<NewsTag>()
                .HasOne(nt => nt.News)
                .WithMany(n => n.NewsTags)
                .HasForeignKey(nt => nt.NewsId);

            modelBuilder.Entity<NewsTag>()
                .HasOne(nt => nt.Tag)
                .WithMany(t => t.NewsTags)
                .HasForeignKey(nt => nt.TagId);

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@citadt.dhhp.edu.vn",
                    Password = "Admin@123", // In a real app, use a password hash
                    FullName = "Administrator",
                    PhoneNumber = "0773171171",
                    IsActive = true,
                    IsAdmin = true,
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    Id = 2,
                    Username = "editor",
                    Email = "editor@citadt.dhhp.edu.vn",
                    Password = "Editor@123",
                    FullName = "Biên Tập Viên",
                    PhoneNumber = "0912345678",
                    IsActive = true,
                    IsAdmin = false,
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    Id = 3,
                    Username = "giangvien",
                    Email = "giangvien@citadt.dhhp.edu.vn",
                    Password = "Giangvien@123",
                    FullName = "Giảng Viên",
                    PhoneNumber = "0987654321",
                    IsActive = true,
                    IsAdmin = false,
                    CreatedAt = DateTime.Now
                }
            );

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Tin tức",
                    Description = "Tin tức mới nhất của Trung tâm",
                    Slug = "tin-tuc",
                    DisplayOrder = 1,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                },
                new Category
                {
                    Id = 2,
                    Name = "Đào tạo",
                    Description = "Thông tin về các khóa đào tạo",
                    Slug = "dao-tao",
                    DisplayOrder = 2,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                },
                new Category
                {
                    Id = 3,
                    Name = "Nghiên cứu khoa học",
                    Description = "Hoạt động nghiên cứu khoa học",
                    Slug = "nghien-cuu-khoa-hoc",
                    DisplayOrder = 3,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                },
                new Category
                {
                    Id = 4,
                    Name = "Chuyển đổi số",
                    Description = "Thông tin về chuyển đổi số",
                    Slug = "chuyen-doi-so",
                    DisplayOrder = 4,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                },
                new Category
                {
                    Id = 5,
                    Name = "Sự kiện",
                    Description = "Các sự kiện của Trung tâm",
                    Slug = "su-kien",
                    DisplayOrder = 5,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                }
            );

            // Seed Tags
            modelBuilder.Entity<Tag>().HasData(
                new Tag { Id = 1, Name = "CNTT", Slug = "cntt", CreatedAt = DateTime.Now },
                new Tag { Id = 2, Name = "Chuyển đổi số", Slug = "chuyen-doi-so", CreatedAt = DateTime.Now },
                new Tag { Id = 3, Name = "Đào tạo", Slug = "dao-tao", CreatedAt = DateTime.Now },
                new Tag { Id = 4, Name = "Chứng chỉ", Slug = "chung-chi", CreatedAt = DateTime.Now },
                new Tag { Id = 5, Name = "Công nghệ", Slug = "cong-nghe", CreatedAt = DateTime.Now },
                new Tag { Id = 6, Name = "IoT", Slug = "iot", CreatedAt = DateTime.Now },
                new Tag { Id = 7, Name = "AI", Slug = "ai", CreatedAt = DateTime.Now },
                new Tag { Id = 8, Name = "Big Data", Slug = "big-data", CreatedAt = DateTime.Now }
            );

            // Seed News
            modelBuilder.Entity<News>().HasData(
                new News
                {
                    Id = 1,
                    Title = "Khai giảng khóa đào tạo CNTT tháng 6/2024",
                    Summary = "Trung tâm Ứng dụng CNTT và Chuyển đổi số tổ chức khai giảng khóa đào tạo mới",
                    Content = "<p>Ngày 01/06/2024, Trung tâm Ứng dụng Công nghệ thông tin và Chuyển đổi số đã tổ chức lễ khai giảng khóa đào tạo Công nghệ thông tin với nhiều chuyên đề hấp dẫn.</p>" +
                              "<p>Các học viên sẽ được tiếp cận với những kiến thức và kỹ năng mới nhất trong lĩnh vực CNTT, giúp nâng cao năng lực cạnh tranh trong thời đại số.</p>" +
                              "<p>Khóa học kéo dài 3 tháng với sự tham gia giảng dạy của các giảng viên có nhiều kinh nghiệm.</p>",
                    Slug = "khai-giang-khoa-dao-tao-cntt-thang-6-2024",
                    FeaturedImage = "/img/news/khai-giang.jpg",
                    IsFeatured = true,
                    IsPublished = true,
                    PublishedAt = new DateTime(2024, 6, 1),
                    ViewCount = 152,
                    CategoryId = 2,
                    AuthorId = 1,
                    CreatedAt = new DateTime(2024, 5, 25)
                },
                new News
                {
                    Id = 2,
                    Title = "Hội thảo về Chuyển đổi số trong giáo dục đại học",
                    Summary = "Trường Đại học Hải Phòng tổ chức hội thảo về chuyển đổi số trong giáo dục đại học",
                    Content = "<p>Ngày 15/5/2024, Trường Đại học Hải Phòng phối hợp với Trung tâm Ứng dụng Công nghệ thông tin và Chuyển đổi số tổ chức hội thảo với chủ đề \"Chuyển đổi số trong giáo dục đại học\".</p>" +
                              "<p>Hội thảo đã thu hút sự tham gia của nhiều chuyên gia hàng đầu trong lĩnh vực giáo dục và công nghệ thông tin. Các bài tham luận đã đề cập đến những thách thức và cơ hội trong quá trình chuyển đổi số tại các trường đại học Việt Nam.</p>" +
                              "<p>Thông qua hội thảo, các đại biểu đã chia sẻ những kinh nghiệm quý báu và đề xuất các giải pháp thiết thực để thúc đẩy quá trình chuyển đổi số trong môi trường giáo dục đại học.</p>",
                    Slug = "hoi-thao-ve-chuyen-doi-so-trong-giao-duc-dai-hoc",
                    FeaturedImage = "/img/news/hoi-thao.jpg",
                    IsFeatured = true,
                    IsPublished = true,
                    PublishedAt = new DateTime(2024, 5, 15),
                    ViewCount = 243,
                    CategoryId = 4,
                    AuthorId = 1,
                    CreatedAt = new DateTime(2024, 5, 10)
                },
                new News
                {
                    Id = 3,
                    Title = "Lịch thi chứng chỉ Tin học tháng 7/2024",
                    Summary = "Thông báo lịch thi chứng chỉ Tin học ứng dụng A, B tháng 7/2024",
                    Content = "<p>Trung tâm Ứng dụng Công nghệ thông tin và Chuyển đổi số thông báo lịch thi chứng chỉ Tin học ứng dụng A, B tháng 7/2024 như sau:</p>" +
                              "<ul>" +
                              "<li>Thời gian: 8h00 ngày 20/7/2024</li>" +
                              "<li>Địa điểm: Phòng máy tính - Tầng 2 Nhà B3, Trường Đại học Hải Phòng</li>" +
                              "<li>Hạn đăng ký: 15/7/2024</li>" +
                              "</ul>" +
                              "<p>Các học viên đăng ký tại Văn phòng Trung tâm, tầng 2 Nhà B3 hoặc qua email: citadt@dhhp.edu.vn</p>",
                    Slug = "lich-thi-chung-chi-tin-hoc-thang-7-2024",
                    FeaturedImage = "/img/news/chung-chi.jpg",
                    IsFeatured = false,
                    IsPublished = true,
                    PublishedAt = new DateTime(2024, 6, 20),
                    ViewCount = 105,
                    CategoryId = 2,
                    AuthorId = 2,
                    CreatedAt = new DateTime(2024, 6, 15)
                },
                new News
                {
                    Id = 4,
                    Title = "Giới thiệu dự án ứng dụng AI trong quản lý đào tạo",
                    Summary = "Trung tâm triển khai dự án ứng dụng AI trong quản lý đào tạo",
                    Content = "<p>Trung tâm Ứng dụng Công nghệ thông tin và Chuyển đổi số vừa triển khai dự án ứng dụng trí tuệ nhân tạo (AI) trong quản lý đào tạo tại Trường Đại học Hải Phòng.</p>" +
                              "<p>Dự án nhằm tự động hóa các quy trình quản lý đào tạo, hỗ trợ công tác ra quyết định và cải thiện trải nghiệm học tập của sinh viên. Hệ thống sẽ có khả năng phân tích dữ liệu lớn, đưa ra các dự báo về kết quả học tập và đề xuất các biện pháp cải thiện.</p>" +
                              "<p>Giai đoạn đầu của dự án dự kiến sẽ hoàn thành vào cuối năm 2024 và sẽ được triển khai thử nghiệm tại một số khoa của trường.</p>",
                    Slug = "gioi-thieu-du-an-ung-dung-ai-trong-quan-ly-dao-tao",
                    FeaturedImage = "/img/news/ai-project.jpg",
                    IsFeatured = true,
                    IsPublished = true,
                    PublishedAt = new DateTime(2024, 6, 10),
                    ViewCount = 178,
                    CategoryId = 3,
                    AuthorId = 3,
                    CreatedAt = new DateTime(2024, 6, 5)
                },
                new News
                {
                    Id = 5,
                    Title = "Thông báo tuyển sinh các khóa đào tạo CNTT tháng 8/2024",
                    Summary = "Trung tâm thông báo tuyển sinh các khóa đào tạo CNTT tháng 8/2024",
                    Content = "<p>Trung tâm Ứng dụng Công nghệ thông tin và Chuyển đổi số thông báo tuyển sinh các khóa đào tạo CNTT tháng 8/2024 như sau:</p>" +
                              "<ol>" +
                              "<li>Khóa học Lập trình Web với ASP.NET Core MVC</li>" +
                              "<li>Khóa học Phát triển ứng dụng di động với React Native</li>" +
                              "<li>Khóa học Phân tích dữ liệu với Python</li>" +
                              "<li>Khóa học IoT cơ bản</li>" +
                              "</ol>" +
                              "<p>Thời gian học: Tối thứ 3, 5, 7 hàng tuần</p>" +
                              "<p>Địa điểm: Phòng học - Tầng 2 Nhà B3, Trường Đại học Hải Phòng</p>" +
                              "<p>Liên hệ đăng ký: Văn phòng Trung tâm, tầng 2 Nhà B3 hoặc hotline: 0773.171.171</p>",
                    Slug = "thong-bao-tuyen-sinh-cac-khoa-dao-tao-cntt-thang-8-2024",
                    FeaturedImage = "/img/news/tuyen-sinh.jpg",
                    IsFeatured = false,
                    IsPublished = true,
                    PublishedAt = new DateTime(2024, 7, 5),
                    ViewCount = 82,
                    CategoryId = 2,
                    AuthorId = 2,
                    CreatedAt = new DateTime(2024, 7, 1)
                }
            );

            // Seed NewsTags
            modelBuilder.Entity<NewsTag>().HasData(
                // For News Id = 1
                new NewsTag { Id = 1, NewsId = 1, TagId = 1 },
                new NewsTag { Id = 2, NewsId = 1, TagId = 3 },
                new NewsTag { Id = 3, NewsId = 1, TagId = 4 },

                // For News Id = 2
                new NewsTag { Id = 4, NewsId = 2, TagId = 2 },
                new NewsTag { Id = 5, NewsId = 2, TagId = 5 },

                // For News Id = 3
                new NewsTag { Id = 6, NewsId = 3, TagId = 1 },
                new NewsTag { Id = 7, NewsId = 3, TagId = 4 },

                // For News Id = 4
                new NewsTag { Id = 8, NewsId = 4, TagId = 7 },
                new NewsTag { Id = 9, NewsId = 4, TagId = 5 },
                new NewsTag { Id = 10, NewsId = 4, TagId = 8 },

                // For News Id = 5
                new NewsTag { Id = 11, NewsId = 5, TagId = 1 },
                new NewsTag { Id = 12, NewsId = 5, TagId = 3 },
                new NewsTag { Id = 13, NewsId = 5, TagId = 6 }
            );
        }
    }
}