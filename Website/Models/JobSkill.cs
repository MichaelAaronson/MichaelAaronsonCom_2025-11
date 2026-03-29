using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models;

public partial class JobSkill
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? Count { get; set; }

    public string? Comments { get; set; }

    public string? Summary { get; set; }

    [NotMapped]
    public int TotalMonths => JobDetailSkills
        .Where(jds => jds.JobDetail?.Job != null)
        .Select(jds => jds.JobDetail.Job!)
        .DistinctBy(job => job.Id)
        .Sum(job => job.Duration);

    [NotMapped]
    public int TotalCount => JobDetailSkills
        .Where(jds => jds.JobDetail?.Job != null)
        .Select(jds => jds.JobDetail.Job!)
        .DistinctBy(job => job.Id)
        .Count();

    public virtual ICollection<JobDetailJobSkill> JobDetailSkills { get; set; } = new List<JobDetailJobSkill>();
}
