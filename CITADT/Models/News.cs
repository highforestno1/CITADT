using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CITADT.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string? ImageUrl { get; set; }

        [StringLength(500)]
        public string? Summary { get; set; }

        [StringLength(200)]
        public string? Slug { get; set; }

        [StringLength(500)]
        public string? FeaturedImage { get; set; }

        public bool IsFeatured { get; set; } = false;

        public bool IsPublished { get; set; } = true;

        public DateTime? PublishedAt { get; set; }

        public int ViewCount { get; set; } = 0;

        public int? CategoryId { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        [ForeignKey("AuthorId")]
        public virtual IdentityUser Author { get; set; }

        public virtual ICollection<NewsTag>? NewsTags { get; set; }
    }
}