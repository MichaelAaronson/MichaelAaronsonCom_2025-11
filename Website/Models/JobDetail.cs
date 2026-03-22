using System;
using System.Collections.Generic;

namespace Website.Models;

public partial class JobDetail
{
    public int Id { get; set; }

    public int? JobId { get; set; }

    public int? Sequence { get; set; }

    public string? Description { get; set; }

    //public virtual Job? Job { get; set; }

    public virtual ICollection<JobDetailSkill> JobDetailSkills { get; set; } = new List<JobDetailSkill>();
}
