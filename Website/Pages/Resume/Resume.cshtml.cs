using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages;

public class ResumeModel : PageModel
{
    private readonly WebsiteContext _context;

    public ResumeModel(WebsiteContext context)
    {
        _context = context;
    }

    public IList<Job> Jobs { get; set; } = default!;
    public int? StartYear { get; set; }

    public async Task OnGetAsync(int? startYear)
    {
        StartYear = startYear;

        Jobs = await _context.Job
            .Include(j => j.JobDetails.OrderBy(jd => jd.Sequence))
            .OrderBy(j => j.Id)
            .ToListAsync();

        if (StartYear.HasValue)
        {
            string cutoff = $"{StartYear.Value:D4}";
            Jobs = Jobs
                .Where(j => string.Compare(j.StartDate, cutoff, StringComparison.Ordinal) >= 0)
                .ToList();
        }
    }
}
