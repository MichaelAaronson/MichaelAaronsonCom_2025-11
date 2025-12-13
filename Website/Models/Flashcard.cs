using System;
using System.Collections.Generic;

namespace Website.Models;

public partial class Flashcard
{
    public int Id { get; set; }

    public bool Learned { get; set; }

    public byte Important { get; set; }

    public string English { get; set; } = null!;

    public string Maori { get; set; } = null!;

    public int? Sequence { get; set; }

    public string? Tag { get; set; }
}
