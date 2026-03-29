using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.Resume
{
    public class SkillsModel : PageModel
    {
        private readonly WebsiteContext _context;

        public SkillsModel(WebsiteContext context)
        {
            _context = context;
        }

        public IList<JobSkill> JobSkill { get;set; } = default!;
        public string CurrentSort { get; set; } = "title";  // ← new

        public async Task OnGetAsync(string? sort)  // ← add parameter
        {
            CurrentSort = sort ?? "title";  // ← new

            //JobSkill = await _context.JobSkill.ToListAsync();
            JobSkill = await _context
                .JobSkill
                .Include(s => s.JobDetailSkills)
                .ThenInclude(jds => jds.JobDetail)
                .ThenInclude(jd => jd.Job)
                .ToListAsync();

            JobSkill = CurrentSort switch
            {
                "title" => JobSkill.OrderBy(s => s.Title).ToList(),
                "count" => JobSkill.OrderByDescending(s => s.TotalCount).ToList(),  // ← new
                _ => JobSkill.OrderByDescending(s => s.TotalMonths).ToList()
            };

            JobSkill = JobSkill.Where(s => s.Count > 0 || s.TotalMonths > 0).ToList();  // ← new line


        }
    }
}
