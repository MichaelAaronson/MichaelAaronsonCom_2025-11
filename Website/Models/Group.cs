using System.ComponentModel.DataAnnotations;
namespace Website.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(500)]
        public string? Description { get; set; }
        public ICollection<PersonGroup> PersonGroups { get; set; } = new List<PersonGroup>();


    }
}
