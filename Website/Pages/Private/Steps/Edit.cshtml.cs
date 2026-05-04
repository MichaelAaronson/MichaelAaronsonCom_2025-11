using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Website.Data;
using Website.Models;

namespace Website.Pages.Private.Steps;

public class EditModel : PageModel
{
    private readonly WebsiteContext _context;

    public EditModel(WebsiteContext context)
    {
        _context = context;
    }

    // Editable fields, bound individually so the Edit form cannot
    // overpost any field that isn't in the form (Pattern B).
    public int Id { get; set; }
    [BindProperty] public string Title { get; set; } = string.Empty;
    [BindProperty] public string? Comments { get; set; }
    [BindProperty] public int DomainId { get; set; }
    [BindProperty] public int? ProjectId { get; set; }
    [BindProperty] public int Priority { get; set; }
    [BindProperty] public DateOnly StartDate { get; set; }
    [BindProperty] public bool IsComplete { get; set; }

    [BindProperty(SupportsGet = true)]
    public string ReturnUrl { get; set; } = "/Data/Steps";

    public SelectList DomainOptions { get; set; } = default!;
    public SelectList ProjectOptions { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var step = await _context.Step.FirstOrDefaultAsync(s => s.Id == id);
        if (step is null)
        {
            return NotFound();
        }

        Id = step.Id;
        Title = step.Title;
        Comments = step.Comments;
        DomainId = step.DomainId;
        ProjectId = step.ProjectId;
        Priority = step.Priority;
        StartDate = step.StartDate;
        IsComplete = step.IsComplete;

        await PopulateDropdownsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            Id = id.Value;
            await PopulateDropdownsAsync();
            return Page();
        }

        var step = await _context.Step.FirstOrDefaultAsync(s => s.Id == id);
        if (step is null)
        {
            return NotFound();
        }

        step.Title = Title;
        step.Comments = Comments;
        step.DomainId = DomainId;
        step.ProjectId = ProjectId;
        step.Priority = Priority;
        step.StartDate = StartDate;
        step.IsComplete = IsComplete;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Step.Any(e => e.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return Redirect(string.IsNullOrEmpty(ReturnUrl) ? "/Private/Steps" : ReturnUrl);
    }

    public async Task<IActionResult> OnPostDeleteAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var step = await _context.Step.FirstOrDefaultAsync(s => s.Id == id);
        if (step is null)
        {
            return NotFound();
        }

        _context.Step.Remove(step);
        await _context.SaveChangesAsync();

        return Redirect(string.IsNullOrEmpty(ReturnUrl) ? "/Private/Steps" : ReturnUrl);
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
