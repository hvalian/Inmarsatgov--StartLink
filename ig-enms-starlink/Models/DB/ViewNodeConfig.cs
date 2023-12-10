using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class ViewNodeConfig
{
    public int Nodeid { get; set; }

    public string Configkey { get; set; } = null!;

    public string? Value { get; set; }
}
