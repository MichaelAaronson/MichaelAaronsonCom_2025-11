using System;
using System.Collections.Generic;

namespace Website.Models;

public partial class Flashcards202504
{
    public int Id { get; set; }

    public double Sequence { get; set; }

    public bool Important { get; set; }

    public bool Learned { get; set; }

    public string English { get; set; } = null!;

    public string Maori { get; set; } = null!;

    public string PowerAppsId { get; set; } = null!;
}
