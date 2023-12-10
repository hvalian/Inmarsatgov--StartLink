using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbSvnUdrNodeMetricValueMap
{
    public long Id { get; set; }

    public string SvnUdrId { get; set; } = null!;

    public long NodeMetricValueId { get; set; }

    public int Svn { get; set; }

    public DateTime DateCreated { get; set; }
}
