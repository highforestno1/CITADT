using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CITADT.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Slug { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property
        public virtual ICollection<NewsTag> NewsTags { get; set; }
    }
} 