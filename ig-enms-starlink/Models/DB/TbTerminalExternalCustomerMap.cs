using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbTerminalExternalCustomerMap
{
    public int Id { get; set; }

    public int NodeId { get; set; }

    public string? ExternalCustomerId { get; set; }

    public string? ExternalCustomerName { get; set; }

    public string? ExternalCustomerType { get; set; }

    public DateTime? DateImported { get; set; }

    public virtual TbNode Node { get; set; } = null!;
}
