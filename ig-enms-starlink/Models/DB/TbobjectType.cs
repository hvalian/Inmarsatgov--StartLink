using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbobjectType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public string Description { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public virtual ICollection<TbobjectConfig> TbobjectConfigs { get; set; } = new List<TbobjectConfig>();

    public virtual ICollection<Tbobject> Tbobjects { get; set; } = new List<Tbobject>();
}
