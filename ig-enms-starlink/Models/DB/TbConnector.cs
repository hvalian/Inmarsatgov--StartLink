using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbConnector
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public int PlatformId { get; set; }

    public string? PlatformLocation { get; set; }

    public string Description { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? ConnectorCreds { get; set; }

    public string? ProcessorName { get; set; }

    public int? PollingInterval { get; set; }

    public DateTime? LastPollDate { get; set; }

    public DateTime? NextPollDate { get; set; }

    public virtual TbPlatform Platform { get; set; } = null!;

    public virtual ICollection<TbNode> TbNodes { get; set; } = new List<TbNode>();

    public virtual ICollection<TbVar> TbVars { get; set; } = new List<TbVar>();

    public virtual ICollection<Tbobject> Tbobjects { get; set; } = new List<Tbobject>();
}
