using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Website.Models;

public partial class Job
{
    public int Id { get; set; }

    [StringLength(255)]
    public string? Company { get; set; }

    [StringLength(255)]
    public string? Location { get; set; }

    [StringLength(100)]
    public string? Dates { get; set; }

    [StringLength(50)]
    public string? StartDate { get; set; }

    [StringLength(50)]
    public string? EndDate { get; set; }

    [StringLength(255)]
    public string? Role { get; set; }
}
