using System.ComponentModel.DataAnnotations;

namespace Website.Models;

public class Image
{
    public int Id { get; set; }

    [Required]
    [StringLength(500)]
    public string Location { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }

    public DateTime? DateTaken { get; set; }

    // Navigation property for many-to-many with Person
    public ICollection<PersonImage> PersonImages { get; set; } = new List<PersonImage>();
}