using System;
using System.Collections.Generic;

namespace Website.Models;

public partial class Project
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int DomainId { get; set; }
}
