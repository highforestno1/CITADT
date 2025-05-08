using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CITADT.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [StringLength(450)]
        public string? Slug { get; set; }

        public int? ParentId { get; set; }

        public int DisplayOrder { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<News>? News { get; set; }
    }
} 