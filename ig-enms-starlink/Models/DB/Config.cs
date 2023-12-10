using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class Config
{
    public int Id { get; set; }

    public string ConfigName { get; set; } = null!;

    public string? ConfigValue { get; set; }

    public bool Active { get; set; }
}
