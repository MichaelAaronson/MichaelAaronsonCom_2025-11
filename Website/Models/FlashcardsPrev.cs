using System;
using System.Collections.Generic;

namespace Website.Models;

public partial class FlashcardsPrev
{
    public bool Learned { get; set; }

    public string ImportantPrev { get; set; } = null!;

    public string English { get; set; } = null!;

    public string Maori { get; set; } = null!;

    public double Sequence { get; set; }

    public string PowerAppsId { get; set; } = null!;

    public int Id { get; set; }

    public bool Important { get; set; }
}
