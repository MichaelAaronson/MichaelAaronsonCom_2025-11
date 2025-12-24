using System;
using System.Collections.Generic;

namespace Website.Models;

public partial class Step
{
    public int Id { get; set; }

    public int Priority { get; set; }

    public DateOnly StartDate { get; set; }

    public bool IsComplete { get; set; }

    public string Title { get; set; } = null!;

    public string? Comments { get; set; }

    public int ProjectId { get; set; }

    public byte[] TimeStamp { get; set; } = null!;
}
