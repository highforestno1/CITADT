using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CITADT.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Summary { get; set; }

        public string Content { get; set; }

        [StringLength(200)]
        public string Slug { get; set; }

        [StringLength(500)]
        public string FeaturedImage { get; set; }

        public bool IsFeatured { get; set; } = false;

        public bool IsPublished { get; set; } = false;

        public DateTime? PublishedAt { get; set; }

        public int ViewCount { get; set; } = 0;

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [ForeignKey("User")]
        public int AuthorId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; }
        public virtual User Author { get; set; }
        public virtual ICollection<NewsTag> NewsTags { get; set; }
    }
}