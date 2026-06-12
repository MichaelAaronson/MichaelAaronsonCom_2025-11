using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Website.Data;   // adjust to your WebsiteContext namespace

namespace Website.Api;

public static class PlanningApi
{
    private static readonly JsonSerializerOptions JsonOpts = new(JsonSerializerOptions.Web)
    {
        // This allows characters like <, >, &, ', and " to be included in the JSON without being escaped.
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
    public static void MapPlanningApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api")
            .RequireAuthorization();

        // GET /api/steps          → incomplete steps (the default working set)
        // GET /api/steps?includeComplete=true
        group.MapGet("/steps", async (WebsiteContext db, bool includeComplete = false) =>
        {
            var steps = await db.Step
                .Where(s => includeComplete || !s.IsComplete)
                .OrderBy(s => s.Domain!.Title)
                .ThenBy(s => s.Project!.Title)
                .ThenBy(s => s.Priority)
                .Select(s => new StepDto(
                    s.Id,
                    s.Title,
                    s.Comments,
                    s.Priority,
                    s.StartDate,
                    s.IsComplete,
                    s.Domain!.Title,
                    s.Project != null ? s.Project.Title : null))
                .ToListAsync();

            var json = steps.Count == 0
                ? "[]"
                : "[\n  " +
                  string.Join(",\n  ", steps.Select(s => JsonSerializer.Serialize(s, JsonOpts))) +
                  "\n]";

            return Results.Text(json, "application/json");
        });
    }

    public record StepDto(
        int Id,
        string Title,
        string? Comments,
        int Priority,
        DateOnly StartDate,
        bool IsComplete,
        string Domain,
        string? Project);
}