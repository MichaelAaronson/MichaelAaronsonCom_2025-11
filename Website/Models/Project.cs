using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Website.Models;

public partial class Project
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public int? GoalId { get; set; }
    public Goal? Goal { get; set; } = null;

    public int DomainId { get; set; }
    public Domain? Domain { get; set; } = null;    
    public ICollection<Step> Steps { get; set; } = [];
}
