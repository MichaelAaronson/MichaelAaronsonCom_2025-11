using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.Private
{
    [Authorize]
    public class PlanningModel : PageModel
    {
        private readonly WebsiteContext _context;

        public PlanningModel(WebsiteContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public bool ShowCompleted { get; set; } = false;

        // Domain -> Projects -> Steps hierarchy
        public List<DomainGroup> Domains { get; set; } = [];

        public int TotalSteps { get; set; }

        public class DomainGroup
        {
            public string DomainTitle { get; set; } = string.Empty;
            public List<ProjectGroup> Projects { get; set; } = [];
        }

        public class ProjectGroup
        {
            public int ProjectId { get; set; }
            public string ProjectTitle { get; set; } = string.Empty;
            public string? GoalTitle { get; set; }
            public int StepsTotal { get; set; }
            public int StepsComplete { get; set; }
            public int StepsActive { get; set; }
            public int StepsDeferred { get; set; }
            public List<Step> Steps { get; set; } = [];
        }

        public async Task OnGetAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            var query = _context.Step
                .Include(s => s.Project)
                    .ThenInclude(p => p!.Domain)
                .Include(s => s.Project)
                    .ThenInclude(p => p!.Goal)
                .AsQueryable();

            if (!ShowCompleted)
            {
                query = query.Where(s => !s.IsComplete);
            }

            var steps = await query
                .OrderBy(s => s.Priority)
                .ThenBy(s => s.StartDate)
                .ToListAsync();

            TotalSteps = steps.Count;

            // Group by Domain then Project
            Domains = steps
                .GroupBy(s => s.Project?.Domain?.Title ?? "(No Domain)")
                .OrderBy(g => g.Key)
                .Select(domainGroup => new DomainGroup
                {
                    DomainTitle = domainGroup.Key,
                    Projects = domainGroup
                        .GroupBy(s => new
                        {
                            ProjectId = s.Project?.Id ?? 0,
                            ProjectTitle = s.Project?.Title ?? "(No Project)",
                            GoalTitle = s.Project?.Goal?.Title
                        })
                        .OrderBy(g => g.Key.ProjectTitle)
                        .Select(projGroup =>
                        {
                            // Count all steps for this project (not just filtered)
                            var allProjectSteps = projGroup.ToList();
                            return new ProjectGroup
                            {
                                ProjectId = projGroup.Key.ProjectId,
                                ProjectTitle = projGroup.Key.ProjectTitle,
                                GoalTitle = projGroup.Key.GoalTitle,
                                StepsTotal = allProjectSteps.Count,
                                StepsComplete = allProjectSteps.Count(s => s.IsComplete),
                                StepsActive = allProjectSteps.Count(s => !s.IsComplete && s.StartDate <= today),
                                StepsDeferred = allProjectSteps.Count(s => !s.IsComplete && s.StartDate > today),
                                Steps = allProjectSteps
                            };
                        })
                        .ToList()
                })
                .ToList();
        }
    }
}