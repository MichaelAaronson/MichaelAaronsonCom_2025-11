using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Website.Models;

public partial class Step
{
    public int Id { get; set; }

    public int Priority { get; set; }

    public DateOnly StartDate { get; set; }

    public bool IsComplete { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;
    public string? Comments { get; set; }

    public int ProjectId { get; set; }
    public Project? Project { get; set; } = null;

    public int DomainId { get; set; }
    public Domain? Domain { get; set; } = null;
}
