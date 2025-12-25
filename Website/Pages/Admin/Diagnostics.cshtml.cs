using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website.Pages.Admin
{
    public class DiagnosticsModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public DiagnosticsModel(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public string? EnvironmentName { get; set; }
        public string? MachineName { get; set; }
        public string? ContentRootPath { get; set; }
        public string? WebRootPath { get; set; }
        public string? ContentConnectionServer { get; set; }
        public string? AspNetCoreEnvironment { get; set; }
        public DateTime ServerTime { get; set; }
        public void OnGet()
        {
            EnvironmentName = _environment.EnvironmentName;
            MachineName = Environment.MachineName;
            ContentRootPath = _environment.ContentRootPath;
            WebRootPath = _environment.WebRootPath;
            ServerTime = DateTime.Now;
            AspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // Extract just the server name from connection string (don't expose full string)
            var sConn = _configuration.GetConnectionString("WebsiteConnection");
            //var connString = _configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(sConn))
            {
                ContentConnectionServer = "NullOrEmpty";
            }
            else
            {
                //ContentConnectionServer = sConn;
                var match = System.Text.RegularExpressions.Regex.Match(sConn, @"Initial Catalog=([^;]+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                ContentConnectionServer = match.Success ? match.Groups[1].Value : "Initial Catalog not found in connection string";
            }
        }
    }
}
