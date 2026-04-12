using System.ComponentModel.DataAnnotations;

namespace Website.Models;

public class ContentBlock
{
    public int Id { get; set; }

    /// <summary>
    /// Unique key to identify this content block, e.g. "about-page", "bio-intro"
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Human-readable title for the editor list
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The content stored as Markdown
    /// </summary>
    public string? MarkdownContent { get; set; }

    /// <summary>
    /// When this content was last modified
    /// </summary>
    public DateTime LastModified { get; set; } = DateTime.UtcNow;
}