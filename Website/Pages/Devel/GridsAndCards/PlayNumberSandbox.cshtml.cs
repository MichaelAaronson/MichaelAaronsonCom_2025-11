using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Website.Models;

namespace Website.Pages.Devel.GridsAndCards
{
    public class PlayNumberSandboxModel : PageModel
    {
        public List<PlayNumber> PlayNumbers { get; set; } = [];

        public void OnGet()
        {
            PlayNumbers =
            [
                new() { Id = 1, Name = "One", Value = 1 },
                new() { Id = 2, Name = "Two — a deliberately longer name to test wrapping", Value = 2 },
                new() { Id = 3, Name = "Three", Value = 3 },
                new() { Id = 4, Name = "Four", Value = 5 },
                new() { Id = 5, Name = "Five", Value = 5 },
            ];
        }
    }
}
