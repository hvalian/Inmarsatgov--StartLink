using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class VwGetAccountsByUserName
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public bool Internal { get; set; }

    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? AccountNumber { get; set; }

    public string? ServiceLineNumber { get; set; }

    public int? NodeId { get; set; }

    public string? TerminalId { get; set; }
}
