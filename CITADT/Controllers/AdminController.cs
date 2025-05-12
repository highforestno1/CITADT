using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CITADT.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using CITADT.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CITADT.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ILogger<AdminController> logger, IConfiguration configuration, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, bool rememberMe)
        {
            var adminUsername = _configuration["AdminUser:Username"];
            var adminPassword = _configuration["AdminUser:Password"];
            var adminRole = _configuration["AdminUser:Role"];

            if (string.IsNullOrEmpty(adminUsername) || string.IsNullOrEmpty(adminPassword))
            {
                _logger.LogError("Admin credentials not configured properly");
                TempData["ErrorMessage"] = "System configuration error";
                return View();
            }

            if (username == adminUsername && password == adminPassword)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, adminRole ?? "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = rememberMe,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                _logger.LogInformation($"User {username} logged in successfully");
                TempData["SuccessMessage"] = "Đăng nhập thành công!";
                return RedirectToAction("Index");
            }

            _logger.LogWarning($"Failed login attempt for user {username}");
            TempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng";
            return View();
        }

        public IActionResult Index()
        {
            // Đếm số lượng tin tức, danh mục, người dùng
            ViewBag.NewsCount = _context.News.Count();
            ViewBag.CategoriesCount = _context.Categories.Count();
            ViewBag.UsersCount = _userManager.Users.Count();
            
            // Lấy 5 tin tức mới nhất
            ViewBag.LatestNews = _context.News
                .OrderByDescending(n => n.CreatedAt)
                .Take(5)
                .ToList();
                
            return View();
        }

        public IActionResult Users()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public IActionResult News()
        {
            var news = _context.News
                .Include(n => n.Category)
                .Include(n => n.Author)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();
            return View(news);
        }

        public IActionResult Categories()
        {
            var categories = _context.Categories
                .OrderBy(c => c.DisplayOrder)
                .ToList();
            return View(categories);
        }
        
        public IActionResult Settings()
        {
            return View();
        }
        
        // Trang tạo mới
        public IActionResult CreateNews()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateNews(News news)
        {
            if (ModelState.IsValid)
            {
                news.AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                news.CreatedAt = DateTime.Now;
                news.Slug = GenerateSlug(news.Title);
                
                if (news.IsPublished)
                {
                    news.PublishedAt = DateTime.Now;
                }
                
                _context.News.Add(news);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã thêm tin tức thành công!";
                return RedirectToAction("News");
            }
            
            ViewBag.Categories = _context.Categories.ToList();
            return View(news);
        }
        
        public IActionResult CreateCategory()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedAt = DateTime.Now;
                category.Slug = GenerateSlug(category.Name);
                
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã thêm danh mục thành công!";
                return RedirectToAction("Categories");
            }
            
            return View(category);
        }
        
        public IActionResult CreateUser()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true
                };
                
                var result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                    TempData["SuccessMessage"] = "Đã thêm người dùng thành công!";
                    return RedirectToAction("Users");
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        // Helper method to generate slug
        private string GenerateSlug(string title)
        {
            var slug = title.ToLower()
                .Replace(" ", "-")
                .Replace("đ", "d")
                .Replace("á", "a").Replace("à", "a").Replace("ả", "a").Replace("ã", "a").Replace("ạ", "a")
                .Replace("ă", "a").Replace("ắ", "a").Replace("ằ", "a").Replace("ẳ", "a").Replace("ẵ", "a").Replace("ặ", "a")
                .Replace("â", "a").Replace("ấ", "a").Replace("ầ", "a").Replace("ẩ", "a").Replace("ẫ", "a").Replace("ậ", "a")
                .Replace("é", "e").Replace("è", "e").Replace("ẻ", "e").Replace("ẽ", "e").Replace("ẹ", "e")
                .Replace("ê", "e").Replace("ế", "e").Replace("ề", "e").Replace("ể", "e").Replace("ễ", "e").Replace("ệ", "e")
                .Replace("í", "i").Replace("ì", "i").Replace("ỉ", "i").Replace("ĩ", "i").Replace("ị", "i")
                .Replace("ó", "o").Replace("ò", "o").Replace("ỏ", "o").Replace("õ", "o").Replace("ọ", "o")
                .Replace("ô", "o").Replace("ố", "o").Replace("ồ", "o").Replace("ổ", "o").Replace("ỗ", "o").Replace("ộ", "o")
                .Replace("ơ", "o").Replace("ớ", "o").Replace("ờ", "o").Replace("ở", "o").Replace("ỡ", "o").Replace("ợ", "o")
                .Replace("ú", "u").Replace("ù", "u").Replace("ủ", "u").Replace("ũ", "u").Replace("ụ", "u")
                .Replace("ư", "u").Replace("ứ", "u").Replace("ừ", "u").Replace("ử", "u").Replace("ữ", "u").Replace("ự", "u")
                .Replace("ý", "y").Replace("ỳ", "y").Replace("ỷ", "y").Replace("ỹ", "y").Replace("ỵ", "y");
            
            // Remove special characters
            slug = System.Text.RegularExpressions.Regex.Replace(slug, "[^a-z0-9-]", "");
            
            // Remove multiple dashes
            slug = System.Text.RegularExpressions.Regex.Replace(slug, "-+", "-");
            
            return slug;
        }
    }
}
