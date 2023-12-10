using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbobjectConfigValue
{
    public int Id { get; set; }

    public int ObjectId { get; set; }

    public int ObjectTypeId { get; set; }

    public string ConfigKey { get; set; } = null!;

    public int ObjectConfigId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string ConfigValueType { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public string? Value { get; set; }

    public string ValueUnit { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public virtual Tbobject Object { get; set; } = null!;

    public virtual TbobjectConfig ObjectConfig { get; set; } = null!;

    public virtual TbobjectType ObjectType { get; set; } = null!;
}
