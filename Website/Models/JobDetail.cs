using System;
using System.Collections.Generic;

namespace Website.Models;

public partial class JobDetail
{
    public int DetailId { get; set; }

    public int? JobId { get; set; }

    public int? Sequence { get; set; }

    public string? Description { get; set; }

    public byte[] SsmaTimeStamp { get; set; } = null!;

    public virtual ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
}
