using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbobjectConfig
{
    public int Id { get; set; }

    public int ObjectId { get; set; }

    public int ObjectTypeId { get; set; }

    public string ConfigKey { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string ConfigValueType { get; set; } = null!;

    public string? SampleValue { get; set; }

    public string ValueUnit { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public virtual Tbobject Object { get; set; } = null!;

    public virtual TbobjectType ObjectType { get; set; } = null!;
}
