using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbenmsGroup
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public bool? Active { get; set; }

    public string CustomerId { get; set; } = null!;

    public int ParentGroupId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? Udmapping { get; set; }

    public int GroupTypeId { get; set; }

    public virtual TbenmsGroupType GroupType { get; set; } = null!;

    public virtual ICollection<TbterminalenmsGroup> TbterminalenmsGroups { get; set; } = new List<TbterminalenmsGroup>();
}
