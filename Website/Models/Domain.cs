using System;
using System.Collections.Generic;

namespace Website.Models;

public partial class Domain
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }
}
