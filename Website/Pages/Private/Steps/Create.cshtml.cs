using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.Private.Steps;

public class CreateModel : PageModel
{
    private readonly WebsiteContext _context;

    public CreateModel(WebsiteContext context)
    {
        _context = context;
    }

    // Editable fields, bound individually so the form cannot
    // overpost any field that isn't in the form (Pattern B).
    [BindProperty] public string Title { get; set; } = string.Empty;
    [BindProperty] public string? Comments { get; set; }
    [BindProperty] public int DomainId { get; set; }
    [BindProperty] public int? ProjectId { get; set; }
    [BindProperty] public int Priority { get; set; } = 1;
    [BindProperty] public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    [BindProperty] public bool IsComplete { get; set; }
    [BindProperty(SupportsGet = true)]
    public string ReturnUrl { get; set; } = "/Data/Steps";

    public SelectList DomainOptions { get; set; } = default!;
    public SelectList ProjectOptions { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        await PopulateDropdownsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync();
            return Page();
        }

        var step = new Step
        {
            Title = Title,
            Comments = Comments,
            DomainId = DomainId,
            ProjectId = ProjectId,
            Priority = Priority,
            StartDate = StartDate,
            IsComplete = IsComplete
        };

        _context.Step.Add(step);
        await _context.SaveChangesAsync();

        return Redirect(string.IsNullOrEmpty(ReturnUrl) ? "/Data/Steps" : ReturnUrl);
    }

    private async Task PopulateDropdownsAsync()
    {
        var domains = await _context.Domain
            .OrderBy(d => d.Title)
            .ToListAsync();
        DomainOptions = new SelectList(domains, nameof(Domain.Id), nameof(Domain.Title));

        var projects = await _context.Project
            .OrderBy(p => p.Title)
            .ToListAsync();
        ProjectOptions = new SelectList(projects, nameof(Project.Id), nameof(Project.Title));
    }
}
