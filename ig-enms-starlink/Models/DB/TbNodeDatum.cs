using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbNodeDatum
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Active { get; set; }

    public int Nodetypeid { get; set; }

    public int Connectorid { get; set; }

    public string? CollectStats { get; set; }
}
