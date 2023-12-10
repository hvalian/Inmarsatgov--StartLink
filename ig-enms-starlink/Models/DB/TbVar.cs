using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbVar
{
    public int Id { get; set; }

    public int VarId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public string Description { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public int ConnectorId { get; set; }

    public virtual TbConnector Connector { get; set; } = null!;

    public virtual ICollection<TbSddcle> TbSddcles { get; set; } = new List<TbSddcle>();
}
