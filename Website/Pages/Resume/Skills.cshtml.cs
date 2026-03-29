using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public async Task OnGetAsync()
        {
            //JobSkill = await _context.JobSkill.ToListAsync();
            JobSkill = await _context
                .JobSkill
                .Include(s => s.JobDetailSkills)
                .ThenInclude(jds => jds.JobDetail)
                .ThenInclude(jd => jd.Job)
                .ToListAsync();

        }
    }
}
