using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Website.Models;

public partial class PlayNumber
{
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    public int Value { get; set; }
}
