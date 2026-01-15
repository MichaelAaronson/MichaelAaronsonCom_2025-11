using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class PersonGroup
    {
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;

        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;

        [StringLength(50)]
        [Display(Name = "Role")]
        public string? Role { get; set; }  // e.g., "Member", "Leader", "Admin"

        [StringLength(500)]
        [Display(Name = "Details")]
        public string? Details { get; set; }
    }
}
