using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbNodeParent
{
    public int Id { get; set; }

    public int NodeId { get; set; }

    public int ParentnodeId { get; set; }

    public bool? Active { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public virtual TbNode Node { get; set; } = null!;

    public virtual TbNode Parentnode { get; set; } = null!;
}
