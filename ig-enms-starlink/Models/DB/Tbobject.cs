using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class Tbobject
{
    public int ObjectId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public int ObjectTypeId { get; set; }

    public int ConnectorId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? ObjectData { get; set; }

    public string? CreatedBy { get; set; }

    public virtual TbConnector Connector { get; set; } = null!;

    public virtual TbobjectType ObjectType { get; set; } = null!;

    public virtual ICollection<TbobjectConfig> TbobjectConfigs { get; set; } = new List<TbobjectConfig>();
}
