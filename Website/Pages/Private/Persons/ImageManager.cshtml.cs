using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website.Pages.Private.Persons
{
    [Authorize]
    public class ImageManagerModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;

        public ImageManagerModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public List<ImageInfo> Images { get; set; } = new();
        public string? Message { get; set; }

        public class ImageInfo
        {
            public string Filename { get; set; } = string.Empty;
            public long SizeKb { get; set; }
        }

        public void OnGet()
        {
            LoadImages();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                Message = "Please select a file to upload.";
                LoadImages();
                return Page();
            }

            var imagesPath = Path.Combine(_environment.WebRootPath, "Images", "Persons");
            Directory.CreateDirectory(imagesPath);

            var filename = Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine(imagesPath, filename);

            // Check if file already exists
            if (System.IO.File.Exists(filePath))
            {
                Message = $"File '{filename}' already exists. Delete it first or rename your file.";
                LoadImages();
                return Page();
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            Message = $"Uploaded: {filename}";
            LoadImages();
            return Page();
        }

        public IActionResult OnPostDelete(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                Message = "No filename specified.";
                LoadImages();
                return Page();
            }

            // Sanitize filename to prevent directory traversal
            filename = Path.GetFileName(filename);

            var imagesPath = Path.Combine(_environment.WebRootPath, "Images", "Persons");
            var filePath = Path.Combine(imagesPath, filename);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                Message = $"Deleted: {filename}";
            }
            else
            {
                Message = $"File not found: {filename}";
            }

            LoadImages();
            return Page();
        }

        private void LoadImages()
        {
            var imagesPath = Path.Combine(_environment.WebRootPath, "Images", "Persons");

            if (Directory.Exists(imagesPath))
            {
                var files = Directory.GetFiles(imagesPath)
                    .Where(f => IsImageFile(f))
                    .OrderBy(f => Path.GetFileName(f));

                foreach (var file in files)
                {
                    var info = new FileInfo(file);
                    Images.Add(new ImageInfo
                    {
                        Filename = Path.GetFileName(file),
                        SizeKb = info.Length / 1024
                    });
                }
            }
        }

        private static bool IsImageFile(string path)
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return ext is ".jpg" or ".jpeg" or ".png" or ".gif" or ".webp";
        }
    }
}

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace Website.Pages.Private.Persons
//{
//    public class ImageManagerModel : PageModel
//    {
//        public void OnGet()
//        {
//        }
//    }
//}
