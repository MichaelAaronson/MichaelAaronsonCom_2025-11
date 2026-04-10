using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.Iptc;

namespace Website.Pages.Data.Persons
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

        // Diagnostic: all metadata for a selected image
        public string? DiagFilename { get; set; }
        public List<MetadataTag> DiagTags { get; set; } = new();

        public class ImageInfo
        {
            public string Filename { get; set; } = string.Empty;
            public long SizeKb { get; set; }
            public string? Description { get; set; }
            public string? DateTaken { get; set; }
        }

        public class MetadataTag
        {
            public string Directory { get; set; } = string.Empty;
            public string TagName { get; set; } = string.Empty;
            public string? Value { get; set; }
        }

        public void OnGet()
        {
            LoadImages();
        }

        public IActionResult OnGetDiag(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                filename = Path.GetFileName(filename);
                var imagesPath = Path.Combine(_environment.WebRootPath, "Images", "Persons");
                var filePath = Path.Combine(imagesPath, filename);

                if (System.IO.File.Exists(filePath))
                {
                    DiagFilename = filename;
                    try
                    {
                        var directories = ImageMetadataReader.ReadMetadata(filePath);
                        foreach (var directory in directories)
                        {
                            foreach (var tag in directory.Tags)
                            {
                                DiagTags.Add(new MetadataTag
                                {
                                    Directory = directory.Name,
                                    TagName = tag.Name,
                                    Value = tag.Description
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Message = $"Error reading metadata: {ex.Message}";
                    }
                }
                else
                {
                    Message = $"File not found: {filename}";
                }
            }

            LoadImages();
            return Page();
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
            System.IO.Directory.CreateDirectory(imagesPath);

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

            if (System.IO.Directory.Exists(imagesPath))
            {
                var files = System.IO.Directory.GetFiles(imagesPath)
                    .Where(f => IsImageFile(f))
                    .OrderBy(f => Path.GetFileName(f));

                foreach (var file in files)
                {
                    var info = new FileInfo(file);
                    var imageInfo = new ImageInfo
                    {
                        Filename = Path.GetFileName(file),
                        SizeKb = info.Length / 1024
                    };

                    ReadMetadata(file, imageInfo);
                    Images.Add(imageInfo);
                }
            }
        }

        private static void ReadMetadata(string filePath, ImageInfo imageInfo)
        {
            try
            {
                var directories = ImageMetadataReader.ReadMetadata(filePath);

                // Try EXIF ImageDescription first
                var exifIfd0 = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
                if (exifIfd0 != null)
                {
                    imageInfo.Description = exifIfd0.GetDescription(ExifDirectoryBase.TagImageDescription);
                }

                // If no EXIF description, try IPTC Caption/Abstract
                if (string.IsNullOrEmpty(imageInfo.Description))
                {
                    var iptc = directories.OfType<IptcDirectory>().FirstOrDefault();
                    if (iptc != null)
                    {
                        imageInfo.Description = iptc.GetDescription(IptcDirectory.TagCaption);
                    }
                }

                // Date taken from EXIF SubIFD (DateTimeOriginal)
                var exifSubIfd = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                if (exifSubIfd != null)
                {
                    var dateTaken = exifSubIfd.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);
                    if (!string.IsNullOrEmpty(dateTaken))
                    {
                        imageInfo.DateTaken = dateTaken;
                    }
                }

                // Fallback: try EXIF IFD0 DateTime
                if (string.IsNullOrEmpty(imageInfo.DateTaken) && exifIfd0 != null)
                {
                    var dateTime = exifIfd0.GetDescription(ExifDirectoryBase.TagDateTime);
                    if (!string.IsNullOrEmpty(dateTime))
                    {
                        imageInfo.DateTaken = dateTime;
                    }
                }
            }
            catch
            {
                // If metadata reading fails, just leave fields null
            }
        }

        private static bool IsImageFile(string path)
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return ext is ".jpg" or ".jpeg" or ".png" or ".gif" or ".webp";
        }
    }
}
