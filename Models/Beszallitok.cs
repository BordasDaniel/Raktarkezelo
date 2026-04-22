using System;
using System.Collections.Generic;

namespace Raktárkezelő.Models;

public partial class Beszallitok
{
    public int Id { get; set; }

    public string? Nev { get; set; }

    public string? Cim { get; set; }

    public string? Telefon { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Termekek>? Termekeks { get; set; } = new List<Termekek>();
}
