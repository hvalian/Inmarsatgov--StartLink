using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbTerminalServicePlan
{
    public int Id { get; set; }

    public string TspObjId { get; set; } = null!;

    public string? TspObjName { get; set; }

    public DateTime? DateImported { get; set; }

    public int NodeId { get; set; }

    public virtual TbNode Node { get; set; } = null!;
}
