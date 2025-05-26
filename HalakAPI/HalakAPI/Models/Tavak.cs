﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HalakAPI.Models;

public partial class Tavak
{
    public int Id { get; set; }

    public string Nev { get; set; } = null!;

    public string Helyszin { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Halak?> Halaks { get; set; } = new List<Halak>();
}
