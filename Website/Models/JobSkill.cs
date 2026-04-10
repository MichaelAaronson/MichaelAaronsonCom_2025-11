using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models;

public partial class JobSkill
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;
    public string? Version { get; set; }
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

    public ICollection<JobDetailJobSkill> JobDetailSkills { get; set; } = [];
}