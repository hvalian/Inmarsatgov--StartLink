using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbobjectProcessLog
{
    public int Id { get; set; }

    public int ObjectId { get; set; }

    public int ObjectTypeId { get; set; }

    public string? ProcessName { get; set; }

    public string LogType { get; set; } = null!;

    public string? Payload { get; set; }

    public DateTime? DateAdded { get; set; }

    public string? Status { get; set; }

    public int? ObjectProcessId { get; set; }

    public virtual Tbobject Object { get; set; } = null!;

    public virtual TbobjectType ObjectType { get; set; } = null!;
}
