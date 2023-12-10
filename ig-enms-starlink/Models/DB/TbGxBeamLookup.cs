using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbGxBeamLookup
{
    public string? ObjId { get; set; }

    public string? ObjAttributesBeamIndex { get; set; }

    public string? ObjAttributesRadius { get; set; }

    public string? ObjName { get; set; }

    public string? ObjAttributesElevation { get; set; }

    public string? ObjAttributesAzimuth { get; set; }
}
