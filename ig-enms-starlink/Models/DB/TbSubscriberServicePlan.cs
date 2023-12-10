using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbSubscriberServicePlan
{
    public int Id { get; set; }

    public string SspObjId { get; set; } = null!;

    public string? SspObjName { get; set; }

    public int? TspId { get; set; }

    public DateTime? DateImported { get; set; }

    public int? SspnodeId { get; set; }

    public virtual TbNode? Sspnode { get; set; }
}
