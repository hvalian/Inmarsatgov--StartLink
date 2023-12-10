using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbGxSddlookup
{
    public int Id { get; set; }

    public string LookupField { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string? Description { get; set; }

    public short? Isactive { get; set; }
}
