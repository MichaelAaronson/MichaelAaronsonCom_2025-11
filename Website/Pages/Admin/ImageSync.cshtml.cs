using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Website.Data;
using Microsoft.EntityFrameworkCore;

namespace Website.Pages.Admin
{
    public class ImageSyncModel : PageModel
    {
        private readonly WebsiteContext _context;
        private readonly IWebHostEnvironment _environment;
        public ImageSyncModel(WebsiteContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public List<MatchedImage> Matched { get; set; } = new();
        public List<string> OrphanedImages { get; set; } = new();
        public List<MissingImage> MissingImages { get; set; } = new();
        //public void OnGet()
        //{
        //}

        public async Task OnGetAsync()
        {
            var imageFolder = Path.Combine(_environment.WebRootPath, "Images", "Contacts");

            // Get all image files in the folder
            var imageFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (Directory.Exists(imageFolder))
            {
                imageFiles = Directory.GetFiles(imageFolder)
                    .Select(Path.GetFileName)
                    .Where(f => f != null)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase)!;
            }

            // Get all Person records with ImageFilename set
            var people = await _context.Person
                .Where(p => p.ImageFilename != null && p.ImageFilename != "")
                .ToListAsync();

            var usedFilenames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var person in people)
            {
                if (imageFiles.Contains(person.ImageFilename!))
                {
                    Matched.Add(new MatchedImage
                    {
                        Filename = person.ImageFilename!,
                        PersonName = $"{person.FirstName} {person.LastNme}",
                        Id = person.Id
                    });
                    usedFilenames.Add(person.ImageFilename!);
                }
                else
                {
                    MissingImages.Add(new MissingImage
                    {
                        Filename = person.ImageFilename!,
                        PersonName = $"{person.FirstName} {person.LastNme}",
                        Id = person.Id // Was PersonId
                    });
                }
            }

            // Find orphaned images (in folder but not in database)
            OrphanedImages = imageFiles
                .Where(f => !usedFilenames.Contains(f))
                .OrderBy(f => f)
                .ToList();
        }


        public class MatchedImage
        {
            public string Filename { get; set; } = "";
            public string PersonName { get; set; } = "";
            public int Id { get; set; }
        }

        public class MissingImage
        {
            public string Filename { get; set; } = "";
            public string PersonName { get; set; } = "";
            public int Id { get; set; }
        }

    }
}
