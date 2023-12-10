using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbobjectTypeProcessConfig
{
    public int Id { get; set; }

    public int ObjectTypeId { get; set; }

    public string? ProcessName { get; set; }

    public string IsParentProcess { get; set; } = null!;

    public int ProcessStepNumber { get; set; }

    public short IsActive { get; set; }

    public int? ParentProcessId { get; set; }

    public virtual TbobjectType ObjectType { get; set; } = null!;
}
