using System.ComponentModel.DataAnnotations;

namespace TeamManagementService.Model
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required]
        public string TeamName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
