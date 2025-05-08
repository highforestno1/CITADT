using System.ComponentModel.DataAnnotations;

namespace CITADT.Models
{
    public class Notification
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        public DateTime? ExpiryDate { get; set; }

        public NotificationType Type { get; set; } = NotificationType.General;
    }

    public enum NotificationType
    {
        General,
        Important,
        Warning,
        Success
    }
} 