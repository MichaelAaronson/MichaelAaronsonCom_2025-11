using System;
using System.Collections.Generic;

namespace Website.Models;

public partial class Project
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int GoalId { get; set; }
    public virtual Goal Goal { get; set; } = null!;

    public int DomainId { get; set; }
    public virtual Domain Domain { get; set; } = null!;    
    public ICollection<Step> Steps { get; set; } = [];
}
