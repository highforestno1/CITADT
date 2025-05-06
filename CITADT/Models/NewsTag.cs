using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CITADT.Models
{
    public class NewsTag
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("News")]
        public int NewsId { get; set; }

        [ForeignKey("Tag")]
        public int TagId { get; set; }

        // Navigation properties
        public virtual News News { get; set; }
        public virtual Tag Tag { get; set; }
    }
} 