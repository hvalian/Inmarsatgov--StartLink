using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbSnterminalFailedRecordsDetail
{
    public string? Id { get; set; }

    public string? ReferenceKey { get; set; }

    public string? Operation { get; set; }

    public string? FailedReason { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? UpdatedDateTime { get; set; }
}
