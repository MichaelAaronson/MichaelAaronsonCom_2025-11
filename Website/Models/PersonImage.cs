using System.ComponentModel.DataAnnotations;

namespace Website.Models;

/// <summary>
/// Join entity for many-to-many relationship between Person and Image
/// </summary>
public class PersonImage
{
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;

    public int ImageId { get; set; }
    public Image Image { get; set; } = null!;

    public bool IsMainImage { get; set; }
}