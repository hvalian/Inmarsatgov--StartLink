using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbmodemInfo
{
    public int Id { get; set; }

    public int NodeId { get; set; }

    public string? CustomerId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? ModemType { get; set; }
}
