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

    public byte[] SsmaTimeStamp { get; set; } = null!;

    public virtual ICollection<JobDetail> JobDetails { get; set; } = new List<JobDetail>();
}
