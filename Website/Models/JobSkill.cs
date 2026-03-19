using System;
using System.Collections.Generic;

namespace Website.Models;

public partial class JobSkill
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? Count { get; set; }

    public string? Comments { get; set; }

    public string? Summary { get; set; }

    public virtual ICollection<JobDetailSkill> JobDetailSkills { get; set; } = new List<JobDetailSkill>();
}
