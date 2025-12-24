using System;
using System.Collections.Generic;

namespace Website.Models;

public partial class Person
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastNme { get; set; }

    public string? Company { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Notes { get; set; }

    public string? ImageFilename { get; set; }
}
