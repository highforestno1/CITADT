using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CITADT.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsAdmin { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public string Role { get; set; } = "Admin";

        // Navigation property for News
        public virtual ICollection<News> News { get; set; }
    }
} 