using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbNodeTypeConfig
{
    public int Id { get; set; }

    public int NodeTypeId { get; set; }

    public string ConfigKey { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string ConfigValueType { get; set; } = null!;

    public string? SampleValue { get; set; }

    public string ValueUnit { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? Config { get; set; }

    public virtual TbNodeType NodeType { get; set; } = null!;
}
