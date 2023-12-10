using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbobjectProcessConfig
{
    public int Id { get; set; }

    public int ObjectId { get; set; }

    public int ObjectTypeId { get; set; }

    public string? ProcessName { get; set; }

    public string IsParentProcess { get; set; } = null!;

    public int ProcessStepNumber { get; set; }

    public short IsActive { get; set; }

    public string? Status { get; set; }

    public string? ProcessInfo { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }

    public string? UserName { get; set; }

    public int? ParentProcessId { get; set; }

    public DateTime Timestamp { get; set; }

    public virtual Tbobject Object { get; set; } = null!;

    public virtual TbobjectType ObjectType { get; set; } = null!;
}
