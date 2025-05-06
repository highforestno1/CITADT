using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CITADT.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FeaturedImage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_News_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsTags_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "DisplayOrder", "IsActive", "Name", "ParentId", "Slug", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 6, 16, 35, 6, 555, DateTimeKind.Local).AddTicks(8661), "Tin tức mới nhất của Trung tâm", 1, true, "Tin tức", null, "tin-tuc", null },
                    { 2, new DateTime(2025, 5, 6, 16, 35, 6, 555, DateTimeKind.Local).AddTicks(8663), "Thông tin về các khóa đào tạo", 2, true, "Đào tạo", null, "dao-tao", null },
                    { 3, new DateTime(2025, 5, 6, 16, 35, 6, 555, DateTimeKind.Local).AddTicks(8665), "Hoạt động nghiên cứu khoa học", 3, true, "Nghiên cứu khoa học", null, "nghien-cuu-khoa-hoc", null },
                    { 4, new DateTime(2025, 5, 6, 16, 35, 6, 555, DateTimeKind.Local).AddTicks(8667), "Thông tin về chuyển đổi số", 4, true, "Chuyển đổi số", null, "chuyen-doi-so", null },
                    { 5, new DateTime(2025, 5, 6, 16, 35, 6, 555, DateTimeKind.Local).AddTicks(8668), "Các sự kiện của Trung tâm", 5, true, "Sự kiện", null, "su-kien", null }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "CreatedAt", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 6, 16, 35, 6, 555, DateTimeKind.Local).AddTicks(8750), "CNTT", "cntt" },
                    { 2, new DateTime(2025, 5, 6, 16, 35, 6, 555, DateTimeKind.Local).AddTicks(8751), "Chuyển đổi số", "chuyen-doi-so" },
                    { 3, new DateTime(2025, 5, 6, 16, 35, 6, 555, DateTimeKind.Local).AddTicks(8753), "Đào tạo", "dao-tao" },
                    { 4, new DateTime(2025, 5, 6, 16, 35, 6, 555, DateTimeKind.Local).AddTicks(8754), "Chứng chỉ", "chung-chi" },
                    { 5, new DateTime(2025, 5, 6, 16, 35, 6, 555, DateTimeKind.Local).AddTicks(8755), "Công nghệ", "cong-nghe" },
                    { 6, new DateTime(2025, 5, 6, 16, 35, 6, 555, DateTimeKind.Local).AddTicks(8756), "IoT", "iot" },
                    { 7, new DateTime(2025, 5, 6, 16, 35, 6, 555, DateTimeKind.Local).AddTicks(8758), "AI", "ai" },
                    { 8, new DateTime(2025, 5, 6, 16, 35, 6, 555, DateTimeKind.Local).AddTicks(8759), "Big Data", "big-data" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FullName", "IsActive", "IsAdmin", "Password", "PhoneNumber", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 6, 16, 35, 6, 555, DateTimeKind.Local).AddTicks(8525), "admin@citadt.dhhp.edu.vn", "Administrator", true, true, "Admin@123", "0773171171", null, "admin" },
                    { 2, new DateTime(2025, 5, 6, 16, 35, 6, 555, DateTimeKind.Local).AddTicks(8528), "editor@citadt.dhhp.edu.vn", "Biên Tập Viên", true, false, "Editor@123", "0912345678", null, "editor" },
                    { 3, new DateTime(2025, 5, 6, 16, 35, 6, 555, DateTimeKind.Local).AddTicks(8529), "giangvien@citadt.dhhp.edu.vn", "Giảng Viên", true, false, "Giangvien@123", "0987654321", null, "giangvien" }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "CreatedAt", "FeaturedImage", "IsFeatured", "IsPublished", "PublishedAt", "Slug", "Summary", "Title", "UpdatedAt", "ViewCount" },
                values: new object[,]
                {
                    { 1, 1, 2, "<p>Ngày 01/06/2024, Trung tâm Ứng dụng Công nghệ thông tin và Chuyển đổi số đã tổ chức lễ khai giảng khóa đào tạo Công nghệ thông tin với nhiều chuyên đề hấp dẫn.</p><p>Các học viên sẽ được tiếp cận với những kiến thức và kỹ năng mới nhất trong lĩnh vực CNTT, giúp nâng cao năng lực cạnh tranh trong thời đại số.</p><p>Khóa học kéo dài 3 tháng với sự tham gia giảng dạy của các giảng viên có nhiều kinh nghiệm.</p>", new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/news/khai-giang.jpg", true, true, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "khai-giang-khoa-dao-tao-cntt-thang-6-2024", "Trung tâm Ứng dụng CNTT và Chuyển đổi số tổ chức khai giảng khóa đào tạo mới", "Khai giảng khóa đào tạo CNTT tháng 6/2024", null, 152 },
                    { 2, 1, 4, "<p>Ngày 15/5/2024, Trường Đại học Hải Phòng phối hợp với Trung tâm Ứng dụng Công nghệ thông tin và Chuyển đổi số tổ chức hội thảo với chủ đề \"Chuyển đổi số trong giáo dục đại học\".</p><p>Hội thảo đã thu hút sự tham gia của nhiều chuyên gia hàng đầu trong lĩnh vực giáo dục và công nghệ thông tin. Các bài tham luận đã đề cập đến những thách thức và cơ hội trong quá trình chuyển đổi số tại các trường đại học Việt Nam.</p><p>Thông qua hội thảo, các đại biểu đã chia sẻ những kinh nghiệm quý báu và đề xuất các giải pháp thiết thực để thúc đẩy quá trình chuyển đổi số trong môi trường giáo dục đại học.</p>", new DateTime(2024, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/news/hoi-thao.jpg", true, true, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "hoi-thao-ve-chuyen-doi-so-trong-giao-duc-dai-hoc", "Trường Đại học Hải Phòng tổ chức hội thảo về chuyển đổi số trong giáo dục đại học", "Hội thảo về Chuyển đổi số trong giáo dục đại học", null, 243 },
                    { 3, 2, 2, "<p>Trung tâm Ứng dụng Công nghệ thông tin và Chuyển đổi số thông báo lịch thi chứng chỉ Tin học ứng dụng A, B tháng 7/2024 như sau:</p><ul><li>Thời gian: 8h00 ngày 20/7/2024</li><li>Địa điểm: Phòng máy tính - Tầng 2 Nhà B3, Trường Đại học Hải Phòng</li><li>Hạn đăng ký: 15/7/2024</li></ul><p>Các học viên đăng ký tại Văn phòng Trung tâm, tầng 2 Nhà B3 hoặc qua email: citadt@dhhp.edu.vn</p>", new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/news/chung-chi.jpg", false, true, new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "lich-thi-chung-chi-tin-hoc-thang-7-2024", "Thông báo lịch thi chứng chỉ Tin học ứng dụng A, B tháng 7/2024", "Lịch thi chứng chỉ Tin học tháng 7/2024", null, 105 },
                    { 4, 3, 3, "<p>Trung tâm Ứng dụng Công nghệ thông tin và Chuyển đổi số vừa triển khai dự án ứng dụng trí tuệ nhân tạo (AI) trong quản lý đào tạo tại Trường Đại học Hải Phòng.</p><p>Dự án nhằm tự động hóa các quy trình quản lý đào tạo, hỗ trợ công tác ra quyết định và cải thiện trải nghiệm học tập của sinh viên. Hệ thống sẽ có khả năng phân tích dữ liệu lớn, đưa ra các dự báo về kết quả học tập và đề xuất các biện pháp cải thiện.</p><p>Giai đoạn đầu của dự án dự kiến sẽ hoàn thành vào cuối năm 2024 và sẽ được triển khai thử nghiệm tại một số khoa của trường.</p>", new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/news/ai-project.jpg", true, true, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "gioi-thieu-du-an-ung-dung-ai-trong-quan-ly-dao-tao", "Trung tâm triển khai dự án ứng dụng AI trong quản lý đào tạo", "Giới thiệu dự án ứng dụng AI trong quản lý đào tạo", null, 178 },
                    { 5, 2, 2, "<p>Trung tâm Ứng dụng Công nghệ thông tin và Chuyển đổi số thông báo tuyển sinh các khóa đào tạo CNTT tháng 8/2024 như sau:</p><ol><li>Khóa học Lập trình Web với ASP.NET Core MVC</li><li>Khóa học Phát triển ứng dụng di động với React Native</li><li>Khóa học Phân tích dữ liệu với Python</li><li>Khóa học IoT cơ bản</li></ol><p>Thời gian học: Tối thứ 3, 5, 7 hàng tuần</p><p>Địa điểm: Phòng học - Tầng 2 Nhà B3, Trường Đại học Hải Phòng</p><p>Liên hệ đăng ký: Văn phòng Trung tâm, tầng 2 Nhà B3 hoặc hotline: 0773.171.171</p>", new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "/img/news/tuyen-sinh.jpg", false, true, new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "thong-bao-tuyen-sinh-cac-khoa-dao-tao-cntt-thang-8-2024", "Trung tâm thông báo tuyển sinh các khóa đào tạo CNTT tháng 8/2024", "Thông báo tuyển sinh các khóa đào tạo CNTT tháng 8/2024", null, 82 }
                });

            migrationBuilder.InsertData(
                table: "NewsTags",
                columns: new[] { "Id", "NewsId", "TagId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 3 },
                    { 3, 1, 4 },
                    { 4, 2, 2 },
                    { 5, 2, 5 },
                    { 6, 3, 1 },
                    { 7, 3, 4 },
                    { 8, 4, 7 },
                    { 9, 4, 5 },
                    { 10, 4, 8 },
                    { 11, 5, 1 },
                    { 12, 5, 3 },
                    { 13, 5, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_News_AuthorId",
                table: "News",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_News_CategoryId",
                table: "News",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsTags_NewsId",
                table: "NewsTags",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsTags_TagId",
                table: "NewsTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsTags");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
