using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Website.Models;

public partial class Person
{
    public int Id { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string? LastName { get; set; }

    [StringLength(100)]
    public string? Company { get; set; }
    [StringLength(100)]
    public string? Email { get; set; }
    [StringLength(50)]
    public string? Phone { get; set; }
    public string? Notes { get; set; }
    [StringLength(100)]
    public string? ImageFilename { get; set; }

    public ICollection<PersonGroup> PersonGroups { get; set; } = [];
    public ICollection<PersonImage> PersonImages { get; set; } = [];

}
