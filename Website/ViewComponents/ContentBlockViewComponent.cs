using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Website.Data;

namespace Website.ViewComponents
{
    public class ContentBlockViewComponent : ViewComponent
    {
        private readonly WebsiteContext _context;
        private static readonly MarkdownPipeline _pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Build();

        public ContentBlockViewComponent(WebsiteContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string key)
        {
            var block = await _context.ContentBlock
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Key == key);

            if (block == null || string.IsNullOrWhiteSpace(block.MarkdownContent))
            {
                return Content(string.Empty);
            }

            var html = Markdown.ToHtml(block.MarkdownContent, _pipeline);
            return new HtmlContentViewComponentResult(
                new Microsoft.AspNetCore.Html.HtmlString(html));
        }
    }
}